using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public List<Stock> Inventory { get; set; } = new List<Stock>();

        public override string ToString()
        {
            return $"ID: {ID}\tName: {LocationName}";
        }
    }
}
