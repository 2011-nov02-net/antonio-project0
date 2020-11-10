using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IOrder
    {
        int OrderNumber { get; set; }
        Location LocationPlaced { get; }
        Customer Customer { get; }
        Dictionary<Book, int> Purchase { get; }
        DateTime TimeStamp { get; }

        Order AddOrder();
    }
}
