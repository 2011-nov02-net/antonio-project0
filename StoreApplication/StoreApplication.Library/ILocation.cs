using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ILocation
    {
        IEnumerable<Location> GetInventory();
        IEnumerable<Location> AdjustInventoryForProduct(Product product, int amount);
        IEnumerable<Location> ProcessOrder(Order order);
        string LocationName { get; set; }

    }
}
