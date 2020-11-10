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
        bool LoadData();
        bool SaveData();
        Order PlaceOrderAtLocation();
        Customer CreateNewCustomer();
        Customer FindCustomerByName(string candidate);
        string GetOrderDetails(int orderNumber);
        string GetOrderHistoryByLocation(Location location);
        string GetOrderHistoryByCustomerName(string candidate);         
    }
}
