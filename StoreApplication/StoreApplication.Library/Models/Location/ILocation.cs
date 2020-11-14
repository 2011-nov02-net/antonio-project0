using StoreApplication.Library.Models.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface ILocation
    {
        public int ID { get; }
        public string LocationName { get; set; }
        public static List<Location> Locations { get; set; }
        public List<Stock> Inventory { get; }
    }
}
