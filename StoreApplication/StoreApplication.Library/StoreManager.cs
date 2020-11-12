﻿using StoreApplication.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace StoreApplication.Library
{
    public static class StoreManager
    {
        private static List<Book> _books = new List<Book>();
        private static List<Customer> _customers = new List<Customer>();
        private static List<Order> _orders = new List<Order>();
        private static List<Location> _locations = new List<Location>();


        private static string validCheckerResponse = "";

        public static List<Book> Books { get { return _books; } set { _books = Books; } }
        public static List<Customer> Customers { get { return _customers; } set { _customers = Customers; } }
        public static List<Order> Orders { get { return _orders; } set { _orders = Orders; } }
        public static List<Location> Locations { get { return _locations; } set { _locations = Locations; } }

        public static bool CreateNewCustomer(string newCustomerName, out string message)
        {
            // Check newCustomerName
            if (InputValidation.IsValidCustomerName(newCustomerName, out validCheckerResponse))
            {
                string[] names = newCustomerName.Split(' ');

                // If Valid name
                Customer newCustomer = new Customer(names[0], names[1]);
                _customers.Add(newCustomer);
                validCheckerResponse = $"Success! New Customer [{newCustomer.Name}] with ID [{newCustomer.ID}] has been added.";
                message = validCheckerResponse;
                return true;
            }
            // If invalid name
            message = validCheckerResponse;
            return false;
        }

        public static bool FindCustomerByName(string candidate, out string message)
        {
            if(InputValidation.IsValidCustomerName(candidate, out validCheckerResponse))
            {
                string[] names = candidate.Split(' ');
                foreach(Customer customer in Customers)
                {

                }
            }
            message = validCheckerResponse;
            return false;
        }

        public static bool GetOrderDetails(int orderNumber, out string message)
        {
            throw new NotImplementedException();
        }

        public static bool GetOrderHistoryByCustomerName(string candidate, out string message)
        {
            throw new NotImplementedException();
        }

        public static bool GetOrderHistoryByLocation(Location location, out string message)
        {
            throw new NotImplementedException();
        }

        public static bool LoadData(out string message)
        {
            throw new NotImplementedException();
        }

        public static Order PlaceOrderAtLocation(string locationID, out string message)
        {
            throw new NotImplementedException();
        }

        public static bool SaveData(out string message)
        {
            throw new NotImplementedException();
        }

        public static Location GetLocationByName(string nameOfLocation)
        {
            Location result = new Location();
            foreach(Location l in Locations)
            {
                if(l.LocationName == nameOfLocation)
                {
                    result = l;
                }
            }
            return result;
        }
    }
}
