using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApplication.DataAccess.Entities
{
    public partial class Order
    {
        public Order()
        {
            Orderlines = new HashSet<Orderline>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Orderline> Orderlines { get; set; }
    }
}
