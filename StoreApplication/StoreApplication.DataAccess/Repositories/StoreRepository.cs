using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Repositories
{
    class StoreRepository
    {
        private readonly DbContextOptions<StoreContext> _contextOptions;

        public StoreRepository(DbContextOptions<StoreContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public IEnumerable<Library.Models.Location> GetAllLocations()
        {
            using var context = new StoreContext( _contextOptions);

            IQueryable<Location> dbLocations = context.Locations
                .Include(i => i.Inventories);

            HashSet<Library.Models.Stock> stock = new HashSet<Library.Models.Stock>();
            foreach (Inventory s in dbLocations[0].Inventories)
            {

            }
            var appLocations = dbLocations.Select(s => new Library.Models.Location(s.Id, s.Name, ).ToList();

            return appLocations;
        }


    }
}
