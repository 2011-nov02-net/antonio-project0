using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    class Location : ILocation
    {
        public Dictionary<Book, int> Inventory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string LocationName => throw new NotImplementedException();

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
