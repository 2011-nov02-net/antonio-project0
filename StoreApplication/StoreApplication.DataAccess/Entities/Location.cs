﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess
{
    public partial class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
