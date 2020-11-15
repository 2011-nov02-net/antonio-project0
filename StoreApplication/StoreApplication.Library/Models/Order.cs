using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Order
    {
        public Location LocationPlaced { get; set; }
        public Customer CustomerPlaced { get; set; }
        public List<OrderLine> Purchase { get; set; }

        public DateTime TimeStamp { get; set; }

        public int OrderNumber { get; set; }

        public override string ToString()
        {
            string result =  $"Order Number: {OrderNumber}\nOrder Date: {TimeStamp}\nItems:";
            foreach(OrderLine ol in Purchase)
            {
                result += $"\n\t{ol}";
            }

            return result;
        }

    }
}
