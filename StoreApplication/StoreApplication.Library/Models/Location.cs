using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }

        public static List<Location> Locations = new List<Location>();
        public static List<Order> OrderHistory = new List<Order>();
        public HashSet<Stock> Inventory = new HashSet<Stock>();

        public Location(int id, string name, HashSet<Stock> inventory)
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
                    Inventory.Find(b => b.Book.ISBN == ol.BookISBN).AdjustStock(ol.Quantity);
                }
                OrderHistory.Add(newOrder);
                return true;
            }

            return false;

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
