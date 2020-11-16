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
        void PlaceAnOrderForACustomer();
        void AddACustomer(Customer customer);
        Customer FindCustomerByName(string[] search);
        string GetDetailsForOrder(int ordernumber);
        string GetOrderHistoryByLocationID(int locationID);
        string GetOrderHistoryByCustomer(string[] customerName);
        void Save();

    }
}
