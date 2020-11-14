using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.Library.Models
{
    public class Stock
    {
        public Book Book { get; set; }
        private int Quantity { get; set; }

        public Stock(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
        }

        public void AdjustStock(int amount)
        {
            Quantity += amount;
        }

        public bool CheckStock(int check)
        {
            if (Quantity <= 0)
            {
                return false;
            }
            if ((Quantity + check) < 0)
            {
                return false;
            }
            return true;
        }
    }
}
