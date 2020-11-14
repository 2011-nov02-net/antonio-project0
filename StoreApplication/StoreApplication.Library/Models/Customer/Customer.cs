using StoreApplication.Library.Models.Order;
using System;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public class Customer : ICustomer
    {
        private Location _mylocation;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public int ID { get; }
        public Location MyStoreLocation;
        public static List<Customer> Customers = new List<Customer>();

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

        public override string ToString()
        {
            return $"ID: [{ID}]\tName: [{Name}]\t StoreLocation: [{MyStoreLocation}]";
        }
    }
}
