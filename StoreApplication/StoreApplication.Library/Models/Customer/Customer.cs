using System;

namespace StoreApplication.Library
{
    public class Customer : ICustomer
    {
        private string _first_name;
        private string _last_name;
        private int _id;

        public string FirstName { get; }
        public string LastName { get; }

        public int ID => throw new NotImplementedException();
    }
}
