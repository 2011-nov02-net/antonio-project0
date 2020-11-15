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

        public Location MyStoreLocation;

        public override string ToString()
        {
            return $"\tID: {ID}\tName: {Name}\tMy Store: {MyStoreLocation}";
        }
    }
}
