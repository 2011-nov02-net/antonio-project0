using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Inventory
    {
        public static Library.Models.Stock Map(Entities.Inventory inventory)
        {
            return new Library.Models.Stock
            {
                Book = Mapper_Book.Map(inventory.BookIsbnNavigation),
                Quantity = (int)inventory.Quantity
            };
        }

        public static Entities.Inventory Map(Library.Models.Stock stock)
        {
            return new Entities.Inventory
            {
                BookIsbnNavigation = Mapper_Book.Map(stock.Book),
                Quantity = stock.Quantity,
                BookIsbn = Mapper_Book.Map(stock.Book).Isbn
            };
        }
    }
}
