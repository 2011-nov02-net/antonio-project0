using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreApplication.Library
{
    public class StoreManager : IStoreManager
    {
        private List<Book> _books = new List<Book>();
        List<Customer> _customers = new List<Customer>();
        List<Order> _orders = new List<Order>();
        List<Location> _locations = new List<Location>();

        public List<Book> Books { get { return _books; } set { _books = Books; } }
        public List<Customer> Customers { get { return _customers; } set { _customers = Customers; } }
        public List<Order> Orders { get { return _orders; } set { _orders = Orders; } }
        public List<Location> Locations { get { return _locations; } set { _locations = Locations; } }

        public Customer CreateNewCustomer(string newCustomerName, out string message)
        {
            string[] names = newCustomerName.Split(' ');
            Customer newCustomer = new Customer(names[0], names[1]);
            _customers.Add(newCustomer);
            message = $"Success! New Customer [{newCustomer.Name}] with ID [{newCustomer.ID}] has been added.";
            return newCustomer;
        }

        public Customer FindCustomerByName(string candidate, out string message)
        {
            throw new NotImplementedException();
        }

        public string GetOrderDetails(int orderNumber, out string message)
        {
            throw new NotImplementedException();
        }

        public string GetOrderHistoryByCustomerName(string candidate, out string message)
        {
            throw new NotImplementedException();
        }

        public string GetOrderHistoryByLocation(Location location, out string message)
        {
            throw new NotImplementedException();
        }

        public bool LoadData(out string message)
        {
            throw new NotImplementedException();
        }

        public Order PlaceOrderAtLocation(string locationID, out string message)
        {
            throw new NotImplementedException();
        }

        public bool SaveData(out string message)
        {
            throw new NotImplementedException();
        }
    }
}
