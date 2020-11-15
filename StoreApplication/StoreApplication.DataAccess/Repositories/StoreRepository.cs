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
        private readonly StoreContext  _context;
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
            IQueryable<Location> dbLocations = _context.Locations
                .Include(i => i.Inventories);

            if(search != null)
            {
                dbLocations = dbLocations.Where(i => i.Name.Contains(search));
            }
            return dbLocations.Select(Mapper.MapLocationsWithInventory);
        }

        /// <summary>
        /// Add a new Customer from Models to Database.
        /// </summary>
        /// <param name="customer"> This is the new Model to be put into the database. It only has a firstname and last name.</param>
        public void AddACustomer(Library.Models.Customer customer)
        {
            if(customer.ID != 0)
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



        public Order Map(Library.Models.Order order)
        {
            return new Order {
                Id = order.OrderNumber,
                s
            };
        }

        public Library.Models.Customer FindCustomerByName(string search)
        {
            Customer dbCustomer = _context.Customers
                .Include(l => l.Location)
                .First(c => c.FirstName == search);
            return Mapper.Map(_context.Customers.Find(dbCustomer.Id));
        }

        public string GetDetailsForOrder(int ordernumber)
        {
            Order dbOrder = _context.Orders
                .Include(ol => ol.Orderlines)
                .Include(c => c.Customer)
                .Include(l => l.Location)
                .First(o => o.Id == ordernumber);
            Library.Models.Order o = new Library.Models.Order();

            o.OrderNumber = dbOrder.Id;
            Library.Models.Customer customer = FindCustomerByName(dbOrder.Customer.FirstName);
            Library.Models.Location locationPlaced = Map(dbOrder.Location);

            o.Purchase = new List<Library.Models.OrderLine>();
            foreach(Orderline orli in dbOrder.Orderlines)
            {
                Library.Models.OrderLine toadd = new Library.Models.OrderLine();
                toadd.BookISBN = orli.BookIsbn;
                toadd.ID = orli.Id;
                toadd.Quantity = orli.Quantity;
                toadd.OrderNumber = orli.Order.Id;
                o.Purchase.Add(toadd);
            }
            return $"{o}\n{customer}\t{locationPlaced}";
        }

        public Library.Models.Location Map(Entities.Location location)
        {
            List<Library.Models.Stock> m_stocks = new List<Library.Models.Stock>();
            List<Library.Models.Order> m_orders = new List<Library.Models.Order>();
            foreach(Inventory i in location.Inventories)
            {
                Library.Models.Stock m_stock = new Library.Models.Stock();
                Library.Models.Book m_book = new Library.Models.Book();
                m_book.ISBN = i.BookIsbn;
                m_stock.Book = m_book;
                m_stock.Quantity = (int) i.Quantity;
            }
            return new Library.Models.Location
            {
                LocationName = location.Name,
                ID = location.Id,
                Inventory = m_stocks,
                OrderHistory = m_orders
            };
        }

        public string GetOrderHistoryByLocation(Library.Models.Location location)
        {
            throw new NotImplementedException();
        }

        public string GetOrderHistoryByCustomer(Library.Models.Customer customer)
        {
            throw new NotImplementedException();
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
