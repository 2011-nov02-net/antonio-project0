using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess
{
    public partial class Inventory
    {
        public int LocationId { get; set; }
        public string BookIsbn { get; set; }
        public int? Quantity { get; set; }

        public virtual Book BookIsbnNavigation { get; set; }
        public virtual Location Location { get; set; }
    }
}
