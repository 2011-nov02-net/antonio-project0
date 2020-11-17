using Microsoft.EntityFrameworkCore;
using StoreApplication.DataAccess.Entities;
using StoreApplication.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext _context;

        /// <summary>
        /// A repository managing data access for Store objects,
        /// using Entity Framework.
        /// </summary>
        public StoreRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// This function can take a string or not. If it does then it will return a location by itself.
        /// If it does not contain a string then it returns all the locations in the database mapped to
        /// Library.Models.Location objects to print.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The list of locations</returns>
        public IEnumerable<Library.Models.Location> GetAllLocations(string search = null)
        {
            IQueryable<Location> dbLocations = _context.Locations;

            // This is were we check if it is one location or all
            if (search != null)
            {
                dbLocations = dbLocations.Where(i => i.Name.Contains(search));
            }

            return dbLocations.Select(Mapper_Location.Map);
        }

        /// <summary>
        /// The purpose of this class is to insert new a new order into the database. 
        /// </summary>
        /// <param name="Order">Type Library.Models.Order. It will contain all data about customer, location, and a list of orderlines.</param>
        public void PlaceAnOrderForACustomer(Library.Models.Order m_order)
        {
            // Create the Entity item to be put into the database
            Order order;
            order = Mapper_Order.MapOrderWithOrderLines(m_order);

            // We need to grab the entity objects from the database for the inventory rows for the given location.
            // This is so we can update them accordingly.
            IEnumerable<Inventory> dbStocks = _context.Inventories.Where(i => i.LocationId == m_order.LocationPlaced.ID);

            // Since we are returned all the rows of inventory we need to iterate through each one to update it
            // This is done no matter if there was 1 purchase or many changing the inventory just to be sure 
            // everything is updated correctly.
            foreach (Inventory i in dbStocks)
            {
                // We also need to iterate through all the Library.Models.Stock list for the location
                foreach (Library.Models.Stock stock in m_order.LocationPlaced.Inventory)
                {
                    // An extra measure is taken here just to be sure that only books that exists in the database are being changed.
                    if (stock.Book.ISBN == i.BookIsbn)
                    {
                        // Set the new quantity
                        i.Quantity = stock.Quantity;
                    }
                }
            }

            // Add the new order and orderlines to the database
            _context.Add(order);

            // Save those changes
            Save();
        }

        /// <summary>
        /// Right now this is mainly a helper method when placing the order. This is because this returns a Library.Models.Customer object
        /// That is manipulated by the c# code. The intention was to get the Customer and then set the location and it's inventory
        /// </summary>
        /// <param name="name">Two strings that are valid names.</param>
        /// <returns></returns>
        public Library.Models.Customer GetCustomerWithLocationAndInventory(string[] name)
        {
            // first we create our db customer to check if we find it
            Customer dbCustomer = new Customer();
            try
            {
                // if we do then we assign it to the customer
                dbCustomer = _context.Customers.Include(l => l.Location).First(c => c.FirstName == name[0] && c.LastName == name[1]);
            }
            catch(InvalidOperationException ex) {
                // if we don't then we return null 
                return null;
            }
            // since we found it we map the new customer with the location
            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithLocation(dbCustomer);

            // then we get the stocks for the location
            m_customer.MyStoreLocation.Inventory = GetStocksForLocation(m_customer.MyStoreLocation.ID);

            return m_customer;
        }

        /// <summary>
        /// This is another help method that just gets the stocks for a given location from the db.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of stocks to be assigned to the object that called it.</returns>
        public List<Library.Models.Stock> GetStocksForLocation(int locationID)
        {
            // since it is a location that exists we don't have to do much exception handling and we just get the inventories for the location including the book table
            IEnumerable<Inventory> stocks = _context.Inventories.Include(b => b.BookIsbnNavigation).Where(i => i.LocationId == locationID);
            List<Library.Models.Stock> m_stocks = new List<Library.Models.Stock>();

            // assign each stock from the list of stocks to a model
            foreach (Inventory s in stocks)
            {
                m_stocks.Add(Mapper_Inventory.Map(s));
            }

            return m_stocks;
        }

        /// <summary>
        /// Add a new Customer from Models to Database.
        /// </summary>
        /// <param name="customer"> This is the new Model to be put into the database. It only has a firstname and last name.</param>
        public void AddACustomer(Library.Models.Customer customer)
        {
            // Create the Entity item to be put into the database
            Customer entity = new Customer();

            // Since the database handles the ID setting with identity, we only need to assign the new entity the firstname and the lastname
            // Maybe in the future we could add a way to change the location, but for now the database sets the location to the default 1.
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Id = 0;

            // Add the new entity to the context to send over to the database
            _context.Add(entity);

            // I am using the aproach of sending the data over after each change instead of having a universal save button
            Save();
        }

        /// <summary>
        /// The purpose of this method is to search the db to return a model of the customer object given two strings as names
        /// </summary>
        /// <param name="search">Two strings a first and a last name</param>
        /// <returns>A customer object</returns>
        public Library.Models.Customer FindCustomerByName(string[] search)
        {
            // Search the db and if someone is found assign it if no one was found assign null
            Customer dbCustomer = _context.Customers
                .FirstOrDefault(c => c.FirstName == search[0] && c.LastName == search[1]);
            
            // if it is null exit the method and return null
            if(dbCustomer == null)
            {
                return null;
            }

            return Mapper_Customer.Map(dbCustomer);
        }
        /// <summary>
        /// The purpose of this method is to return the string version of an order, given an order number.
        /// It not only returns the information on the customer but also each order line.
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns>A string version of an order summary.</returns>
        public string GetDetailsForOrder(int ordernumber)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the library with the relevant books
            FillBookLibrary();

            // Create the order objects to be filled
            Order dbOrder;
            Library.Models.Order m_order;

            // Try to see if the order even exists if it does then assign it
            try
            {
                dbOrder = _context.Orders
                    .Include(ol => ol.Orderlines)
                    .Include(c => c.Customer)
                    .Include(l => l.Location)
                    .First(o => o.Id == ordernumber);

                m_order = Mapper_Order.MapOrderWithLocationCustomerAndOrderLines(dbOrder);
            }
            catch (InvalidOperationException ex)
            {
                // if it doesn't then return the error along with a descriptive text
                return $"{ex.Message}\nOrder does not exist!";
            }

            return $"{m_order}\n{m_order.CustomerPlaced}\t{m_order.LocationPlaced}";
        }

        /// <summary>
        /// The purpose is to turn the db information for a given locations order history into readable text.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public string GetOrderHistoryByLocationID(int locationID)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the library with the relevant books
            FillBookLibrary();

            string results = "";

            // Find if the location exists and include all information including orders and their orderlines
            Location dbLocation = _context.Locations
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .FirstOrDefault(l => l.Id == locationID);

            // If it doesn't exist then return that the location does not exist.
            if(dbLocation == null)
            {
                return "This location id does not exist.";
            }
            
            // If it does then map the location to their orders
            Library.Models.Location m_location = Mapper_Location.MapLocationWithOrders(dbLocation);

            // Start building the string with the locatio information first
            results += m_location;
            foreach (Library.Models.Order order in m_location.OrderHistory)
            {
                // Then for each order add them to the string as well.
                results += $"\n{order}";
            }
            if(results == m_location.ToString())
            {
                // If the location exists but no orders have been placed let the user know
                results += "\nNo orders have been placed at this location.";
            }
            return results;
        }

        public string GetOrderHistoryByCustomer(string[] customerName)
        {
            // This method is called because we need the information on the whole catalog
            // Since the catalog is small I went with this implementation.
            // If it was much large I would only fill the library with the relevant books
            FillBookLibrary();

            // Attempt to find the customer
            Customer dbCustomer = _context.Customers
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .FirstOrDefault(c => c.FirstName == customerName[0] && c.LastName == customerName[1]);

            // If the customer was not found then let the user know
            if(dbCustomer == null)
            {
                return "No Customer exists by this name.";
            }
            string result = "";
            // if one was found then map it to a usable object
            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithOrders(dbCustomer);

            // Build the string that needs to be returned
            result += m_customer;
            foreach (Library.Models.Order order in m_customer.Orders)
            {
                result += $"\n{order}";
            }
            return result;
        }

        /// <summary>
        /// The purpose of this class is to fill the static library in the models with the book information
        /// </summary>
        public void FillBookLibrary()
        {
            IEnumerable<Book> dbBooks = _context.Books.ToList();
            foreach (Book b in dbBooks)
            {
                Library.Models.Book.Library.Add(Mapper_Book.Map(b));
            }
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
