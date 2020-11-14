using System;
using System.Collections.Generic;

namespace StoreApplication.Library.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public int ID { get; }
        public Location MyStoreLocation;
        public static List<Customer> Customers = new List<Customer>();
        public List<Order> OrderHistory = new List<Order>();

        public Customer(string firstname, string lastname, int id)
        {
            FirstName = firstname;
            LastName = lastname;
            MyStoreLocation = null;
            ID = id;
        }
        public Customer(string firstname, string lastname, Location location, int id)
        {
            FirstName = firstname;
            LastName = lastname;
            MyStoreLocation = location;
            ID = id;
            Customers.Add(this);
        }

        public static Customer FindCustomerByName(string validCandidate)
        {
            return Customers.Find(c => c.Name == validCandidate);
        }

        public string GetOrderHistroy()
        {
            string list = "";
            foreach (Order o in OrderHistory)
            {
                list += o.ToString();
            }
            return list;
        }

        public override string ToString()
        {
            return $"ID: [{ID}]\tName: [{Name}]\t StoreLocation: [{MyStoreLocation}]";
        }
    }
}
