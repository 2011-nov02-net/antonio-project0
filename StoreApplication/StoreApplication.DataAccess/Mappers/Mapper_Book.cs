using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess.Mappers
{
    public static class Mapper_Book
    {
        /// <summary>
        /// The purpose of this method is to take a db object and turn it into a model object
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static Library.Models.Book Map(Entities.BookEntity book)
        {
            return new Library.Models.Book { 
            AuthorFirstName = book.AuthorFirstName,
            AuthorLastName = book.AuthorLastName,
            ISBN = book.Isbn,
            Price = book.Price,
            Title = book.Name,            
            };
        }

        /// <summary>
        /// The purpose of this method to take a model book and turn it into a db book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static Entities.BookEntity Map(Library.Models.Book book)
        {
            return new Entities.BookEntity
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
