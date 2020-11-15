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

        public static List<Order> OrderHistory { get; }

        public bool AddNewOrderLine(string ISBNAndQuantity)
        {
            string[] lineFiltered = SplitString(ISBNAndQuantity);
            //var newOrderLine;//new OrderLine(lineFiltered[0], Int32.Parse(lineFiltered[1]));
            //Purchase.Add(newOrderLine);
            return true;
        }

        public string[] SplitString(string items)
        {
            items = String.Concat(items.Where(c => !Char.IsWhiteSpace(c)));
            return items.Split(',');
        }

        public decimal GetOrderTotal()
        {
            decimal total = 0;
            foreach(OrderLine o in Purchase)
            {
                total += o.TotalCost;
            }
            return total;
        }

        public List<Order> GetOrderHistoryByLocation(Location location)
        {
            List<Order> orderHistoryForGivenLocation = new List<Order>();
            foreach(Order o in OrderHistory)
            {
                if(o.LocationPlaced == location)
                {
                    orderHistoryForGivenLocation.Add(o);
                }
            }
            return orderHistoryForGivenLocation;
        }

        public List<Order> GetOrderHistoryByCustomer(Customer customer)
        {
            List<Order> orderHistoryForGivenCustomer = new List<Order>();
            foreach (Order o in OrderHistory)
            {
                if (o.Customer == customer)
                {
                    orderHistoryForGivenCustomer.Add(o);
                }
            }
            return orderHistoryForGivenCustomer;
        }
    }
}
