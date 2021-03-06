﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess.Entities
{
    public partial class InventoryEntity
    {
        public int LocationId { get; set; }
        public string BookIsbn { get; set; }
        public int? Quantity { get; set; }

        public virtual BookEntity BookIsbnNavigation { get; set; }
        public virtual LocationEntity Location { get; set; }
    }
}
