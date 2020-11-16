using System;
using System.Collections.Generic;

namespace StoreApplication.Library.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }
        public int ID { get; set; }
        public virtual Location MyStoreLocation { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        public override string ToString()
        {
            return $"ID: {ID}\tName: {Name}";
        }
    }
}
