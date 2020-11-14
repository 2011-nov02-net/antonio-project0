using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models.Order
{
    public class OrderLine
    {
        public string BookISBN { get; }
        public int Quantity { get; }
        public decimal TotalCost {
            get => TotalCost;
            set => TotalCost = Convert.ToDecimal(Quantity * Book.Library.Find(b => b.ISBN == BookISBN).Price);
        }

        public OrderLine(string isbn, int quantity)
        {
            BookISBN = isbn;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"ISBN: {BookISBN}\tQty:{Quantity}\tCost:{TotalCost}";
        }
    }       
}