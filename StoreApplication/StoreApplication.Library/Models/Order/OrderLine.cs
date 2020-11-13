using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models.Order
{
    public class OrderLine
    {
        public int BookID { get; }
        public int Quantity { get; }
        public decimal TotalCost {
            get => TotalCost;
            set => TotalCost = Convert.ToDecimal(Quantity * StoreManager.Books.Find(b => b.ISBN == this.BookID.ToString()).Price);
        }
    }       
}