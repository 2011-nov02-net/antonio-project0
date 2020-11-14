using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess
{
    public partial class Orderline
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string BookIsbn { get; set; }
        public int Quantity { get; set; }

        public virtual Book BookIsbnNavigation { get; set; }
        public virtual Order Order { get; set; }
    }
}
