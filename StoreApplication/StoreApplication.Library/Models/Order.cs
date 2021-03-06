﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Order
    {
        private const int ordercap = 250;
        public Location LocationPlaced { get; set; }
        public Customer CustomerPlaced { get; set; }
        public List<OrderLine> Purchase { get; set; } = new List<OrderLine>();
        public DateTime TimeStamp { get; set; }
        public static List<Order> OrderHistory { get; } = new List<Order>();
        public int OrderNumber { get; set; }

        /// <summary>
        /// If an order is being placed we want to add each orderline to the Purchase object to keep track
        /// </summary>
        /// <param name="ISBNAndQuantity"></param>
        /// <returns></returns>
        public bool AddNewOrderLine(string ISBNAndQuantity)
        {
            string[] lineFiltered = SplitString(ISBNAndQuantity);

            // Check if the book exists in the catalog
            if (!Book.CheckIfIsValidIsbn(lineFiltered[0])) 
            { 
                return false; 
            }
            var newOrderLine = new OrderLine();
            newOrderLine.BookISBN = lineFiltered[0];

            // make sure that the quantity is a number
            try
            {
                Int32.Parse(lineFiltered[1]);
                if (Int32.Parse(lineFiltered[1]) >= ordercap)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            // add the orderline to this order
            newOrderLine.Quantity = Int32.Parse(lineFiltered[1]);
            Purchase.Add(newOrderLine);
            return true;
        }

        /// <summary>
        /// a helper method that turns a string into 2 strings that removes white space
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public string[] SplitString(string items)
        {
            items = String.Concat(items.Where(c => !Char.IsWhiteSpace(c)));
            return items.Split(',');
        }

        /// <summary>
        /// Helper method that gets the whole orders total
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderTotal()
        {
            decimal total = 0;
            foreach (OrderLine o in Purchase)
            {
                total += o.LineCost;
            }
            return total;
        }
        
        /// <summary>
        /// Return a list of orders for a given location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get an order history for a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
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
            string result = $"Order Number: {OrderNumber}\tTotal: {GetOrderTotal()}\tOrder Date: {TimeStamp}\nItems:";
            foreach (OrderLine ol in Purchase)
            {
                result += $"\n\t{ol}";
            }

            return result;
        }
    }
}
