using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Book
    {
        public static Library.Models.Book Map(Entities.Book book)
        {
            return new Library.Models.Book { 
            AuthorFirstName = book.AuthorFirstName,
            AuthorLastName = book.AuthorLastName,
            ISBN = book.Isbn,
            Price = book.Price,
            Title = book.Name,            
            };
        }

        public static Entities.Book Map(Library.Models.Book book)
        {
            return new Entities.Book
            {
                Isbn = book.ISBN,
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                Name = book.ISBN,
                Price = book.Price
            };
        }
    }
}
