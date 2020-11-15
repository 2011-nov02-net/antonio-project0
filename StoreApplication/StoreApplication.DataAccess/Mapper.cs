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
                LocationName = location.Name
            };
        }

        /// <summary>
        /// Maps a restaurant business model to an entity for Entity Framework,
        /// including all reviews if present.
        /// </summary>
        /// <param name="restaurant">The restaurant business model.</param>
        /// <returns>The restaurant entity.</returns>
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

        /// <summary>
        /// Maps an Entity Framework review entity to a business model,
        /// not including the restaurant.
        /// </summary>
        /// <param name="restaurant">The review entity.</param>
        /// <returns>The review business model.</returns>
        public static Library.Models.Location Map(Entities.Location location)
        {
            return new Library.Models.Location
            {
                ID = location.Id,
                LocationName = location.Name,
                Inventory = (System.Collections.Generic.List<Library.Models.Stock>)location.Inventories
            };
        }

        /// <summary>
        /// Maps a review business model to an entity for Entity Framework,
        /// not including the restaurant.
        /// </summary>
        /// <param name="review">The review business model.</param>
        /// <returns>The review entity.</returns>
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
    }
}
