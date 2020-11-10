using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    class Order : IOrder
    {
        private Location _location;
        private Dictionary<Book, int> purchase;
        private DateTime _timestamp;
        public Location LocationPlaced => throw new NotImplementedException();

        public Dictionary<Book, int> Purchase => throw new NotImplementedException();

        public DateTime TimeStamp => throw new NotImplementedException();

        public Order AddOrder()
        {
            throw new NotImplementedException();
        }
    }
}
