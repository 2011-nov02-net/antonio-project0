using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ILocation
    {
        Dictionary<Product, int> GetInventory();
        void AdjustInventoryForProduct(Product product, int amount);
        void ProcessOrder(Order order);
        string LocationName { get; set; }

    }
}
