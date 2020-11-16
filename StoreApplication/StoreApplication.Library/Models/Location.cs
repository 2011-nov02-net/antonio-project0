using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public List<Stock> Inventory { get; set; } = new List<Stock>();

        public bool AttemptOrderAtLocation(Order newOrder, out string message)
        {
            string response = "";
            int attempted = 0;
            foreach (OrderLine ol in newOrder.Purchase)
            {
                if (CheckStockForOrderAttempt(Book.Library.Find(b => b.ISBN == ol.BookISBN), ol.Quantity, out response))
                {
                    response = "\n" + response;
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
            foreach (Stock i in Inventory)
            {
                if (i.Book == book)
                {
                    if (i.CheckStock(amount))
                    {
                        message = $"Enough Books exist for: {book}!";
                        return true;
                    }
                    message = "There is not enough Books!";
                    return false;
                }
            }
            message = "Could not find book";
            return false;
        }

        public override string ToString()
        {
            return $"ID: {ID}\tName: {LocationName}";
        }
    }
}
