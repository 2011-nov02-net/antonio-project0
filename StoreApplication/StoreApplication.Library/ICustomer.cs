using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ICustomer
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
