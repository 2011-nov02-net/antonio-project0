using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApplication.Library.Models;

namespace StoreApplication.Library.Interfaces
{
    public interface IStoreRepository
    {
        IEnumerable<Location> GetAllLocations(string search = null);

    }
}
