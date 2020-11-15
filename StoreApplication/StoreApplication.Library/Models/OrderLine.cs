using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class OrderLine
    {
        public string BookISBN { get; set; }
        public int Quantity { get; set; }
        public int ID { get; set; }
        public int OrderNumber { get; set; }

        public override string ToString()
        {
            return $"ISBN: {BookISBN}\tQty:{Quantity}\t";
        }
    }       
}