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
            Customer dbCustomer = new Customer();
            try
            {
                dbCustomer = _context.Customers.Include(l => l.Location).First(c => c.FirstName == name[0] && c.LastName == name[1]);
            }
            catch(InvalidOperationException ex) {
                return null;
            }
            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithLocation(dbCustomer);
            m_customer.MyStoreLocation.Inventory = GetStocksForLocation(m_customer.MyStoreLocation.ID);

            return m_customer;
        }

        public List<Library.Models.Stock> GetStocksForLocation(int locationID)
        {
            IEnumerable<Inventory> stocks = _context.Inventories.Include(b => b.BookIsbnNavigation).Where(i => i.LocationId == locationID);
            List<Library.Models.Stock> m_stocks = new List<Library.Models.Stock>();

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

        public Library.Models.Customer FindCustomerByName(string[] search)
        {
            Customer dbCustomer = _context.Customers
                .FirstOrDefault(c => c.FirstName == search[0] && c.LastName == search[1]);
            if(dbCustomer == null)
            {
                return null;
            }
            return Mapper_Customer.Map(dbCustomer);
        }

        public string GetDetailsForOrder(int ordernumber)
        {
            FillBookLibrary();
            Order dbOrder;
            Library.Models.Order o;
            try
            {
                dbOrder = _context.Orders
                    .Include(ol => ol.Orderlines)
                    .Include(c => c.Customer)
                    .Include(l => l.Location)
                    .First(o => o.Id == ordernumber);

                o = Mapper_Order.MapOrderWithLocationCustomerAndOrderLines(dbOrder);
            }
            catch (InvalidOperationException ex)
            {
                return $"{ex.Message}\nOrder does not exist!";
            }

            return $"{o}\n{o.CustomerPlaced}\t{o.LocationPlaced}";
        }

        public string GetOrderHistoryByLocationID(int locationID)
        {
            FillBookLibrary();
            string results = "";
            Location dbLocation = _context.Locations
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .First(l => l.Id == locationID);
            Library.Models.Location m_location = Mapper_Location.MapLocationWithOrders(dbLocation);
            results += m_location;
            foreach (Library.Models.Order order in m_location.OrderHistory)
            {
                results += $"\n{order}";
            }
            if(results == m_location.ToString())
            {
                results += "\nNo orders have been placed at this location.";
            }
            return results;
        }

        public string GetOrderHistoryByCustomer(string[] customerName)
        {
            FillBookLibrary();

            Customer dbCustomer = _context.Customers
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .FirstOrDefault(c => c.FirstName == customerName[0] && c.LastName == customerName[1]);
            if(dbCustomer == null)
            {
                return "No Customer exists by this name.";
            }
            string result = "";
            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithOrders(dbCustomer);
            result += m_customer;
            foreach (Library.Models.Order order in m_customer.Orders)
            {
                result += $"\n{order}";
            }
            return result;
        }

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
