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
        void PlaceAnOrderForACustomer(Order m_order);
        public Library.Models.Customer GetCustomerWithLocationAndInventory(int id);
        void AddACustomer(Customer customer);
        public List<Library.Models.Customer> FindCustomerByName(string[] search);
        string GetDetailsForOrder(int ordernumber);
        string GetOrderHistoryByLocationID(int locationID);
        public IEnumerable<Order> GetOrderHistoryByCustomer(int id);
        void Save();

        public List<Library.Models.Stock> GetStocksForLocation(int locationID);
        void FillBookLibrary();


    }
}
