using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class StoreManager : IStoreManager
    {
        public List<Book> Books { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Customer> Customers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Order> Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Location> Locations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Customer CreateNewCustomer()
        {
            throw new NotImplementedException();
        }

        public Customer FindCustomerByName(string candidate)
        {
            throw new NotImplementedException();
        }

        public string GetOrderDetails(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public string GetOrderHistoryByCustomerName(string candidate)
        {
            throw new NotImplementedException();
        }

        public string GetOrderHistoryByLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        public Order PlaceOrderAtLocation()
        {
            throw new NotImplementedException();
        }

        public bool SaveData()
        {
            throw new NotImplementedException();
        }
    }
}
