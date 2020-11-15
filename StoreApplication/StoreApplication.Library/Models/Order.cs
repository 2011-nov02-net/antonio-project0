using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Order
    {
        public Location LocationPlaced { get; set; }

        public List<OrderLine> Purchase { get; set; }

        public DateTime TimeStamp { get; set; }

        public Customer Customer { get; set; }

        public int OrderNumber { get; set; }

        public override string ToString()
        {
            return $"Order Number: {OrderNumber}\t{Customer}";
        }

    }
}
