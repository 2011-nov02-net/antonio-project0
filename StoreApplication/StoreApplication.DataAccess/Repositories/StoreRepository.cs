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
        private readonly DbContextOptions<Entities.StoreContext> _contextOptions;

        public StoreRepository(DbContextOptions<Entities.StoreContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public IEnumerable<Library.Models.Location> GetAllLocations(string search = null)
        {
            using var context = new Entities.StoreContext( _contextOptions);

            IQueryable<Entities.Location> dbLocations = context.Locations
                .Include(i => i.Inventories).AsNoTracking();
            if(search != null)
            {
                dbLocations = dbLocations.Where(i => i.Name.Contains(search));
            }

            var appLocations = dbLocations.Select(s => new Library.Models.Location(s.Id, s.Name, (HashSet<Library.Models.Stock>)s.Inventories)).ToList();

            return appLocations;
        }


    }
}
