using System;
using System.Linq;

namespace StoreApplication.DataAccess
{
    /// <summary>
    /// Maps an Entity Framework restaurant entity to a business model,
    /// including all reviews if present.
    /// </summary>
    /// <param name="restaurant">The restaurant entity.</param>
    /// <returns>The restaurant business model.</returns>

    public static class Mapper
    {
        public static Library.Models.Location MapLocationsWithInventory(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                Inventory = location.Inventories.Select(Mapper.MapInventoriesWithStock).ToList()
            };
        }

        public static Library.Models.Stock MapInventoriesWithStock(Entities.Inventory inventory)
        {
            return new Library.Models.Stock
            {
                Book = new Library.Models.Book { ISBN = inventory.BookIsbn },
                Quantity = (int)inventory.Quantity
            };
        }
        public static Entities.Location MapLocationsWithInventory(Library.Models.Location location)
        {
            return new Entities.Location
            {
                Id = location.ID,
                Name = location.LocationName,
                Orders = (System.Collections.Generic.ICollection<Entities.Order>)location.OrderHistory,
                Inventories = (System.Collections.Generic.ICollection<Entities.Inventory>)location.Inventory
            };
        }

        public static Library.Models.Location MapCustomerWithLocation(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name
            };
        }
        public static Library.Models.Location Map(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                Inventory = (System.Collections.Generic.List<Library.Models.Stock>)location.Inventories
            };
        }

        public static Library.Models.Customer Map(Entities.Customer customer)
        {
            return new Library.Models.Customer
            {
                FirstName = customer.FirstName,
                ID = customer.Id,
                LastName = customer.LastName,
                MyStoreLocation = MapLocationToCustomer(customer.Location)
            };
        }

        public static Library.Models.Location MapLocationToCustomer(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name
            };
        }
        public static Entities.Location Map(Library.Models.Location location)
        {
            return new Entities.Location
            {
                Id = location.ID,
                Name = location.LocationName,
                Orders = (System.Collections.Generic.ICollection<Entities.Order>)location.OrderHistory,
                Inventories = (System.Collections.Generic.ICollection<Entities.Inventory>)location.Inventory
            };
        }
        
        /// <summary>
        /// Maps a customer business model to an entity for Entity Framework
        /// </summary>
        public static Entities.Customer Map(Library.Models.Customer customer)
        {
            return new Entities.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.ID,
                LocationId = customer.MyStoreLocation.ID
            };
        }

        public static Entities.Order Map(Library.Models.Order orders)
        {
            return new Entities.Order
            {
                OrderDate = orders.TimeStamp,
                CustomerId = orders.Customer.ID,
                LocationId = orders.Customer.MyStoreLocation.ID,
                Id = orders.OrderNumber,
                Orderlines = orders.Purchase.Select(Map).ToList()
            };
        }

        public static Entities.Orderline Map(Library.Models.OrderLine orderLine)
        {
            return new Entities.Orderline
            {
                BookIsbn = orderLine.BookISBN,
                Id = orderLine.ID,
                OrderId = orderLine.OrderNumber,
                Quantity = orderLine.Quantity
            };
        }
    }
}
