using StoreApplication.Library.Models.Order;
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
        OrderLine Purchase { get; }
        DateTime TimeStamp { get; }

    }
}
