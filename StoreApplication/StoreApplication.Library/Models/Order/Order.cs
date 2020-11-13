using StoreApplication.Library.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Order : IOrder
    {
        private static int _orderIDSeed = 36954257;
        private int _ordernumber;
        public Location LocationPlaced { get; set; }

        public OrderLine Purchase { get; set; }

        public DateTime TimeStamp { get; set; }

        public Customer Customer { get; set; }

        public int OrderNumber { get => _ordernumber; set { _ordernumber = _orderIDSeed++; } }

        public Order(Location location, OrderLine purchase, DateTime time, Customer customer)
        {
            LocationPlaced = location;
            Purchase = purchase;
            TimeStamp = time;
            Customer = customer;
        }
    }
}
