using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Order
    {
        public static Library.Models.Order MapOrderWithLocationCustomerAndOrderLines(Entities.Order order)
        {
            return new Library.Models.Order
            {
                OrderNumber = order.Id,
                Purchase = order.Orderlines.Select(Mapper_OrderLine.Map).ToList(),
                TimeStamp = (DateTime)order.OrderDate,
                CustomerPlaced = Mapper_Customer.Map(order.Customer),
                LocationPlaced = Mapper_Location.Map(order.Location)
            };
        }
        public static Entities.Order MapOrderWithOrderLines(Library.Models.Order order)
        {
            return new Entities.Order
            {
                Id = order.OrderNumber,
                OrderDate = order.TimeStamp,
                Orderlines = order.Purchase.Select(Mapper_OrderLine.Map).ToList()
            };
        }
        public static Library.Models.Order MapOrderWithOrderLines(Entities.Order order)
        {
            return new Library.Models.Order
            {
                OrderNumber = order.Id,
                Purchase = order.Orderlines.Select(Mapper_OrderLine.Map).ToList(),
                TimeStamp = (DateTime)order.OrderDate
            };
        }

        public static Entities.Order Map(Library.Models.Order order)
        {
            return new Entities.Order
            {
                Id = order.OrderNumber,
                OrderDate = order.TimeStamp
            };
        }
        public static Library.Models.Order Map(Entities.Order order)
        {
            return new Library.Models.Order
            {
                OrderNumber = order.Id,
                TimeStamp = (DateTime)order.OrderDate
            };
        }
    }
}
