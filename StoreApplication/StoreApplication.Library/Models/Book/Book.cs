using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Book : IBook
    {
        public string ISBN { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorFullName { get => $"{AuthorFirstName} {AuthorLastName}"; }
        public decimal Price { get; set; }
        public static List<Book> Library { get; set; }

        public Book(string isbn, string authorLastName, string authorFirstName, decimal price)
        {
            ISBN = isbn;
            AuthorFirstName = authorFirstName;
            AuthorLastName = authorLastName;
            Price = price;
            Library.Add(this);
        }
    }
}
