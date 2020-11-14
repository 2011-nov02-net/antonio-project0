using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess
{
    public partial class Book
    {
        public Book()
        {
            Inventories = new HashSet<Inventory>();
            Orderlines = new HashSet<Orderline>();
        }

        public string Isbn { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Orderline> Orderlines { get; set; }
    }
}
