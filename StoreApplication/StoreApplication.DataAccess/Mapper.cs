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
    public static Library.Models.Location MapLocationsWithInventories(Entities.Location location)
    {
        return new Library.Models.Location
        {
            ID = location.Id,
            LocationName = location.Name,
            Inventory = location.Inventories.Select(Map).ToList()
        };
    }
    public static Library.Models.Stock Map(Entities.Inventory inventory, Entities.Book book)
    {
        return new Library.Models.Stock
        {
            Book = new Library.Models.Book(book.Isbn, book.AuthorLastName, book.AuthorFirstName, book.Price),
            Quantity = (int)inventory.Quantity
        };
    }
}
