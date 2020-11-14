using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IBook
    {
        string ISBN { get; }
        string AuthorLastName { get; }
        string AuthorFirstName { get; }
        string AuthorFullName { get; }
        decimal Price { get; set; }
    }
}
