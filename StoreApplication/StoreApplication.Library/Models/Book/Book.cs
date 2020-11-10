using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    public class Book : IBook
    {
        private string _isbn;
        private string _authorLastName;
        private string _authorFirstName;
        private double _price;

        public string ISBN { get => _isbn; }
        public string AuthorLastName { get => _authorLastName; }
        public string AuthorFirstName { get => _authorFirstName; }
        public string AuthorFullName { get => $"{_authorFirstName} {_authorLastName}"; }
        public double Price { get => _price; }

        public Book(string isbn, string authorLastName, string authorFirstName)
        {
            _isbn = isbn;
            _authorFirstName = authorFirstName;
            _authorLastName = authorLastName;
        }
    }
}
