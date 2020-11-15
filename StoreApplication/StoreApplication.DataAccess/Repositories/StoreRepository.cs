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


    }
}
