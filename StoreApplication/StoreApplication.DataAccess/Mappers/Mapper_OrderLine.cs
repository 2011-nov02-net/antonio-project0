using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Mappers
{
    public static class Mapper_OrderLine
    {
        /// <summary>
        /// Turn an entity orderline into a model orderline
        /// </summary>
        /// <param name="orderline"></param>
        /// <returns></returns>
        public static Library.Models.OrderLine Map(Entities.OrderlineEntity orderline)
        {
            return new Library.Models.OrderLine
            {
                BookISBN = orderline.BookIsbn,
                Quantity = orderline.Quantity,
                LineCost = 0
            };
        }

        /// <summary>
        /// turn a model orderline into an entity orderline
        /// </summary>
        /// <param name="orderline"></param>
        /// <returns></returns>
        public static Entities.OrderlineEntity Map(Library.Models.OrderLine orderline)
        {
            return new Entities.OrderlineEntity
            {
                BookIsbn = orderline.BookISBN,
                Quantity = orderline.Quantity
            };
        }
    }
}
