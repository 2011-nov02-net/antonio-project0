using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Mappers
{
    public static class Mapper_Inventory
    {
        /// <summary>
        /// Turn a entity inventory into a model stock
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static Library.Models.Stock Map(Entities.InventoryEntity inventory)
        {
            return new Library.Models.Stock
            {
                Book = Mapper_Book.Map(inventory.BookIsbnNavigation),
                Quantity = (int)inventory.Quantity
            };
        }
        
        /// <summary>
        /// Turn a model stock into an entity inventory
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public static Entities.InventoryEntity Map(Library.Models.Stock stock)
        {
            return new Entities.InventoryEntity
            {
                BookIsbnNavigation = Mapper_Book.Map(stock.Book),
                Quantity = stock.Quantity,
                BookIsbn = Mapper_Book.Map(stock.Book).Isbn
            };
        }
    }
}
