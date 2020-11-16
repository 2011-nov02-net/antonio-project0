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
        public List<OrderLine> Purchase { get; set; } = new List<OrderLine>();

        public DateTime TimeStamp { get; set; }
        public static List<Order> OrderHistory { get; } = new List<Order>();


        public int OrderNumber { get; set; }
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
            foreach (OrderLine o in Purchase)
            {
                total += o.LineCost;
            }
            return total;
        }

        public List<Order> GetOrderHistoryByLocation(Location location)
        {
            List<Order> orderHistoryForGivenLocation = new List<Order>();
            foreach (Order o in OrderHistory)
            {
                if (o.LocationPlaced == location)
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
                if (o.CustomerPlaced == customer)
                {
                    orderHistoryForGivenCustomer.Add(o);
                }
            }
            return orderHistoryForGivenCustomer;
        }
        public override string ToString()
        {
            string result =  $"Order Number: {OrderNumber}\tTotal: {GetOrderTotal()}\tOrder Date: {TimeStamp}\nItems:";
            foreach(OrderLine ol in Purchase)
            {
                result += $"\n\t{ol}";
            }

            return result;
        }

    }
}
