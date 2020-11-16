using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Location
    {
        /// <summary>
        /// This turns a Location Model into a Location entity, by assigning each relavent property
        /// to a column in the Location table
        /// </summary>
        /// <param name="location">The location model.</param>
        /// <returns new Entity.Location></returns>
        public static Entities.Location MapLocationWithOrders(Library.Models.Location location)
        {
            return new Entities.Location
            {
                Id = location.ID,
                Name = location.LocationName,
                Orders = location.OrderHistory.Select(Mapper_Order.MapOrderWithOrderLines).ToList()
            };
        }

        /// <summary>
        /// This turns a Location Entity into a Location model.
        /// </summary>
        /// <param name="customer">The customer entity.</param>
        /// <returns></returns>
        public static Library.Models.Location MapLocationWithOrders(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                OrderHistory = location.Orders.Select(Mapper_Order.MapOrderWithOrderLines).ToList()
            };
        }

        public static Library.Models.Location MapLocationsWithInventory(Entities.Location location)
        {
            return new Library.Models.Location 
            {
                ID = location.Id,
                LocationName = location.Name,
                Inventory = location.Inventories.Select(Mapper_Inventory.Map).ToList()
            };
        }

        public static Entities.Location Map(Library.Models.Location location)
        {
            return new Entities.Location
            {
                Id = location.ID,
                Name = location.LocationName
            };
        }
        public static Library.Models.Location Map(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name
            };
        }
    }
}
