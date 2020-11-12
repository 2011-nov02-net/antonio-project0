using System;

namespace StoreApplication.Library
{
    public class Customer : ICustomer
    {
        private static int customerIDSeed = 421555;
        private string _firstname;
        private string _lastname;
        private int _id;
        private Location _myStoreLocation;

        public string FirstName { get => _firstname; set => _firstname = value; }
        public string LastName { get => _lastname;  set => _lastname = value;  }
        public string Name { get => FirstName + " " + LastName;  }
        public int ID { get => _id; set => _id = value; }
        public Location MyStoreLocation
        {
            get => _myStoreLocation;
            set
            {
                if (_myStoreLocation == null)
                {
                    _myStoreLocation = new Location();
                }
                else
                {
                    _myStoreLocation = value;
                }
            }
        }
        public Customer(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = customerIDSeed++;
            MyStoreLocation = null;
        }
        public Customer(string firstname, string lastname, string locationName)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = customerIDSeed++;
            MyStoreLocation = StoreManager.GetLocationByName(locationName);
        }
    }
}
