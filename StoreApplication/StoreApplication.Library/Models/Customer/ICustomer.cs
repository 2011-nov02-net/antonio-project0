using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ICustomer
    {
        string FirstName { get; }
        string LastName { get; }
        int ID { get; }
    }
}
