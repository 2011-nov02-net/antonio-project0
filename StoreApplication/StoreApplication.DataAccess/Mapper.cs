using System;

namespace StoreApplication.DataAccess
{
    /// <summary>
    /// Maps an Entity Framework restaurant entity to a business model,
    /// including all reviews if present.
    /// </summary>
    /// <param name="restaurant">The restaurant entity.</param>
    /// <returns>The restaurant business model.</returns>
    public static Library.Models.Location MapLocationsWith(Entities.Restaurant restaurant)
    {
        return new Library.Models.Restaurant
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Reviews = restaurant.Review.Select(Map).ToList()
        };
    }
}
