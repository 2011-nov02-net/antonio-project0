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
        /// Add a new Customer. 
        /// </summary>
        public void AddACustomer(Library.Models.Customer customer)
        {
            if(customer.ID != 0)
            {
                s_logger.Warn($"Customer to be added has an ID ({customer.ID}) already: ignoring.");
            }

            s_logger.Info($"Adding Customer: {customer}");
            Customer entity = new Customer();
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Id = 0;
            _context.Add(entity);
        }

        public Library.Models.Customer FindCustomerByName(string search)
        {
            Customer dbCustomer = _context.Customers
                .Include(l => l.Location)
                .First(c => c.FirstName == search);
            return Mapper.Map(_context.Customers.Find(dbCustomer.Id));
        }

        public Library.Models.Order GetDetailsForOrder(int ordernumber)
        {
            Order dbOrder = _context.Orders
                .Include(ol => ol.Orderlines)
                .Include(c => c.Customer)
                .First(o => o.Id == ordernumber);
            Library.Models.Order o = new Library.Models.Order();
            o.OrderNumber = dbOrder.Id;
            o.Customer = FindCustomerByName(dbOrder.Customer.FirstName);
            foreach(Orderline orli in dbOrder.Orderlines)
            {
                Library.Models.OrderLine toadd = new Library.Models.OrderLine();
                toadd.BookISBN = orli.BookIsbn;
                toadd.ID = orli.Id;
                toadd.Quantity = orli.Quantity;
                toadd.OrderNumber = orli.Order.Id;
                o.Purchase.Add(toadd);
            }
            return o;
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
