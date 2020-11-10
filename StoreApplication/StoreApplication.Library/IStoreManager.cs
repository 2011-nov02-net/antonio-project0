using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IStoreManager
    {
        List<Book> Books { get; set; }
        List<Customer> Customers { get; set; }
        List<Order> Orders { get; set; }
        List<Location> Locations { get; set; }
        bool LoadData(out string message);
        bool SaveData(out string message);
        Order PlaceOrderAtLocation(string locationID, out string message);
        Customer CreateNewCustomer(string newCustomerName, out string message);
        Customer FindCustomerByName(string candidate, out string message);
        string GetOrderDetails(int orderNumber, out string message);
        string GetOrderHistoryByLocation(Location location, out string message);
        string GetOrderHistoryByCustomerName(string candidate, out string message);         
    }
}
