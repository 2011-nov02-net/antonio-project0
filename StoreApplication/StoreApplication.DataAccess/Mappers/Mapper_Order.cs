﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Mappers
{
    public static class Mapper_Order
    {
        /// <summary>
        /// Turn an entity order with the location and customer and it's orderlines into objects
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Library.Models.Order MapOrderWithLocationCustomerAndOrderLines(Entities.OrderEntity order)
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

        /// <summary>
        /// Turn a model object into an entity order with their orderlines
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Entities.OrderEntity MapOrderWithOrderLines(Library.Models.Order order)
        {
            return new Entities.OrderEntity
            {
                Id = order.OrderNumber,
                CustomerId = order.CustomerPlaced.ID,
                LocationId = order.LocationPlaced.ID,
                Orderlines = order.Purchase.Select(Mapper_OrderLine.Map).ToList()
            };
        }

        /// <summary>
        /// Turn a map order with their orderlines into a model order with model orderlines
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Library.Models.Order MapOrderWithOrderLines(Entities.OrderEntity order)
        {
            return new Library.Models.Order
            {
                OrderNumber = order.Id,
                Purchase = order.Orderlines.Select(Mapper_OrderLine.Map).ToList(),
                TimeStamp = (DateTime)order.OrderDate
            };
        }

        public static Entities.OrderEntity Map(Library.Models.Order order)
        {
            return new Entities.OrderEntity
            {
                Id = order.OrderNumber,
                CustomerId = order.CustomerPlaced.ID,
                LocationId = order.LocationPlaced.ID
            };
        }
        public static Library.Models.Order Map(Entities.OrderEntity order)
        {
            return new Library.Models.Order
            {
                OrderNumber = order.Id,
                TimeStamp = (DateTime)order.OrderDate
            };
        }
    }
}
