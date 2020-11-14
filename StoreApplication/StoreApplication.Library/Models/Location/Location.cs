using StoreApplication.Library.Models.Location;
using StoreApplication.Library.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Location : ILocation
    {
        public int ID { get; }
        public string LocationName { get; set; }

        public static List<Location> Locations = new List<Location>();
        public static List<Order> OrderHistory = new List<Order>();
        public List<Stock> Inventory = new List<Stock>();

        public Location(int id, string name, List<Stock> inventory)
        {
            ID = id;
            LocationName = name;
            Inventory = inventory;
            Locations.Add(this);
        }

        public bool AttemptOrderAtLocation(Order newOrder, out string message)
        {
            string response = "";
            int attempted = 0;
            foreach(OrderLine ol in newOrder.Purchase)
            {
                if(CheckStockForOrderAttempt(Book.Library.Find(b => b.ISBN == ol.BookISBN), ol.Quantity, out response))
                {
                    response = "\n"+response;
                    attempted++;
                }
            }
            message = response;

            if (newOrder.Purchase.Count == attempted)
            {
                foreach (OrderLine ol in newOrder.Purchase)
                {
                    if (CheckStockForOrderAttempt(Book.Library.Find(b => b.ISBN == ol.BookISBN), ol.Quantity, out response))
                    {
                        response = "\n" + response;
                        attempted++;
                    }
                }
                PlaceOrder(newOrder);
                return true;
            }

            return false;

        }


        public void PlaceOrder(Order order)
        {
            foreach (OrderLine ol in order.Purchase)
            {
                
            }
            OrderHistory.Add(order);
        }

        public bool CheckStockForOrderAttempt(Book book, int amount, out string message)
        {
            foreach(Stock i in Inventory)
            {
                if(i.Book == book)
                {
                    if (i.CheckStock(amount))
                    {
                        message = $"Enough Books exist for: {book}!";
                        return true;
                    }
                    message =  "There is not enough Books!";
                    return false;
                }
            }
            message = "Could not find book";
            return false;
        }

        public Location GetLocationByName(string name)
        {
            foreach(Location l in Locations)
            {
                if(l.LocationName == name)
                {
                    return l;
                }
            }
            return null;
        }

    }
}
