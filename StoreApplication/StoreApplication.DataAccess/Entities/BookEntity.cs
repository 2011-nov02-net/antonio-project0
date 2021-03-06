﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess.Entities
{
    public partial class BookEntity
    {
        public BookEntity()
        {
            Inventories = new HashSet<InventoryEntity>();
            Orderlines = new HashSet<OrderlineEntity>();
        }

        public string Isbn { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public virtual ICollection<InventoryEntity> Inventories { get; set; }
        public virtual ICollection<OrderlineEntity> Orderlines { get; set; }
    }
}
