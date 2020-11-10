using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ILocation
    {
        Dictionary<Book, int> Inventory { get; set; }
        string LocationName { get; }
        void AdjustInventoryForProduct(Book book, int amount);
        void ProcessOrder(Order order);

    }
}
