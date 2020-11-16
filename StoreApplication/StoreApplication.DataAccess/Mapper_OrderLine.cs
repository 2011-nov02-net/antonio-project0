using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_OrderLine
    {
        public static Library.Models.OrderLine Map(Entities.Orderline orderline)
        {
            return new Library.Models.OrderLine
            {
                BookISBN = orderline.BookIsbn,
                ID = orderline.Id,
                Quantity = orderline.Quantity,
                OrderNumber = orderline.OrderId,
                LineCost = 0
            };
        }

        public static Entities.Orderline Map(Library.Models.OrderLine orderline)
        {
            return new Entities.Orderline
            {
                BookIsbn = orderline.BookISBN,
                Quantity = orderline.Quantity,
                Id = orderline.ID,
                OrderId = orderline.OrderNumber
            };
        }
    }
}
