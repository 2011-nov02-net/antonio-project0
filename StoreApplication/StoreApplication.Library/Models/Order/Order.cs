using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Order : IOrder
    {
        private static int _orderIDSeed = 36954257;
        private Location _location;
        private Dictionary<Book, int> purchase;
        private DateTime _timestamp;
        private Customer _customer;
        private int _ordernumber;
        public Location LocationPlaced { get; set; }

        public Dictionary<Book, int> Purchase { get; set; }

        public DateTime TimeStamp { get; set; }

        public Customer Customer { get; set; }

        public int OrderNumber { get => _ordernumber; set { _ordernumber = _orderIDSeed++; } }

        public Order(Location location, Dictionary<Book, int> purchase, DateTime time, Customer customer)
        {
            LocationPlaced = location;
            Purchase = purchase;
            TimeStamp = time;
            Customer = customer;
        }
        public Order AddOrder()
        {
            throw new NotImplementedException();
        }
    }
}
