using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Order
    {
        public static Entities.Order Map(Library.Models.Order order)
        {
            return new Entities.Order
            {
                Id = order.OrderNumber,
                Location = Mapper_Location.Map(order.LocationPlaced),
                Customer = Mapper_Customer.Map(order.CustomerPlaced),
                CustomerId = Mapper_Customer.Map(order.CustomerPlaced).Id,
                LocationId = Mapper_Location.Map(order.LocationPlaced).Id,
                OrderDate = order.TimeStamp,
                Orderlines = order.Purchase.Select(Mapper_OrderLine.Map).ToList()
            };
        }

        public static Library.Models.Order Map(Entities.Order order)
        {
            return new Library.Models.Order
            {
                LocationPlaced = Mapper_Location.Map(order.Location),
                CustomerPlaced = Mapper_Customer.Map(order.Customer),
                OrderNumber = order.Id,
                Purchase = order.Orderlines.Select(Mapper_OrderLine.Map).ToList(),
                TimeStamp = (DateTime)order.OrderDate
            };
        }
    }
}
