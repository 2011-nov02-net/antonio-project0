using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorFullName { get => $"{AuthorFirstName} {AuthorLastName}"; }
        public decimal Price { get; set; }

        public static List<Book> Library = new List<Book>();

        public static bool CheckIfIsValidIsbn(string candidate)
        {   
            return Library.Any(b => b.ISBN == candidate);
        }

        public override string ToString()
        {
            return $"ISBN{ISBN}\tTitle: {Title}\tAuthor: {AuthorFullName}";
        }
    }
}
