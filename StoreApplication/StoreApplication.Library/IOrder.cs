using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IOrder
    {
        Location LocationPlaced { get; }
        Dictionary<Book, int> Purchase { get; }
        DateTime TimeStamp { get; }

        Order AddOrder();
    }
}
