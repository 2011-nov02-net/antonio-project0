using StoreApplication.Library.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IOrder
    {
        int OrderNumber { get; set; }
        Location LocationPlaced { get; set; }
        Customer Customer { get; set; }
        List<OrderLine> Purchase { get; set; }
        DateTime TimeStamp { get; set; }

    }
}
