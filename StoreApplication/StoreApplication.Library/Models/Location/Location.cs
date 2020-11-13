using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Location : ILocation
    {
        private int _locationID;
        public Dictionary<Book, int> Inventory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string LocationName { get; set; }

        public Location()

        public void AdjustInventoryForProduct(Book book, int amount)
        {
            throw new NotImplementedException();
        }

        public void ProcessOrder(Order order)
        {
            throw new NotImplementedException();
        }

    }
}
