using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// A repository managing data access for restaurant objects and their review members,
        /// using Entity Framework.
        /// </summary>
        /// <remarks>
        /// This class ought to have better exception handling and logging.
        /// </remarks>
        public StoreRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Library.Models.Location> GetAllLocations(string search = null)
        {
            IQueryable<Location> dbLocations = _context.Locations;

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
            order = Mapper_Order.Map(m_order);

            IEnumerable<Inventory> dbStocks = _context.Inventories.Where(i => i.LocationId == m_order.LocationPlaced.ID);
            foreach(Inventory i in dbStocks)
            {
                foreach(Library.Models.Stock stock in m_order.LocationPlaced.Inventory)
                {
                    if(stock.Book.ISBN == i.BookIsbn)
                    {
                        i.Quantity = stock.Quantity;
                    }
                }
            }
            
            _context.SaveChanges();
            _context.Add(order);
            Save();
        }

        public Library.Models.Customer GetCustomerWithLocationAndInventory(string[] name)
        {
            Customer dbCustomer = _context.Customers.Include(l=> l.Location).First(c => c.FirstName == name[0] && c.LastName == name[1]);

            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithLocation(dbCustomer);
            m_customer.MyStoreLocation.Inventory = GetStocksForLocation(m_customer.MyStoreLocation.ID);

            return m_customer;
        }

        public List<Library.Models.Stock> GetStocksForLocation(int locationID)
        {
            IEnumerable<Inventory> stocks = _context.Inventories.Include(b => b.BookIsbnNavigation).Where(i => i.LocationId ==locationID);
            List<Library.Models.Stock> m_stocks = new List<Library.Models.Stock>();

            foreach(Inventory s in stocks)
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
            if (customer.ID != 0)
            {
                s_logger.Warn($"Customer to be added has an ID ({customer.ID}) already: ignoring.");
            }

            s_logger.Info($"Adding Customer: {customer}");

            // Create the Entity item to be put into the database
            Customer entity = new Customer();
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Id = 0;
            _context.Add(entity);
        }

        public Library.Models.Customer FindCustomerByName(string[] search)
        {
            Customer dbCustomer = _context.Customers
                .Include(l => l.Location)
                .First(c => c.FirstName == search[0] && c.LastName == search[1]);

            return Mapper_Customer.MapCustomerWithLocation(_context.Customers.Find(dbCustomer.Id));
        }

        public string GetDetailsForOrder(int ordernumber)
        {
            FillBookLibrary();
            Order dbOrder = _context.Orders
                .Include(ol => ol.Orderlines)
                .Include(c => c.Customer)
                .Include(l => l.Location)
                .First(o => o.Id == ordernumber);
            Library.Models.Order o = Mapper_Order.MapOrderWithLocationCustomerAndOrderLines(dbOrder);

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
            foreach(Library.Models.Order order in m_location.OrderHistory)
            {
                results += $"\n\t{order}";
            }
            return results;
        }

        public string GetOrderHistoryByCustomer(string[] customerName)
        {
            FillBookLibrary();
            
            Customer dbCustomer = _context.Customers
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.Orderlines)
                .First(c => c.FirstName == customerName[0] && c.LastName == customerName[1]);
            string result = "";
            Library.Models.Customer m_customer = Mapper_Customer.MapCustomerWithOrders(dbCustomer);
            result += m_customer;
            foreach(Library.Models.Order order in m_customer.Orders)
            {
                result += $"\n\t{order}";
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
            s_logger.Info("Saving changes to the database");
            _context.SaveChanges();
        }
    }
}
