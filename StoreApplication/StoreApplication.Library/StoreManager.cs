using StoreApplication.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using StoreApplication.Library.Models;

namespace StoreApplication.Library
{
    public static class StoreManager
    {

        private static string validCheckerResponse = "";

        public static bool AttemptToPlaceOrder(Customer customer, Order order, out string message)
        {
            customer.OrderHistory.Add(order);
            return customer.MyStoreLocation.AttemptOrderAtLocation(order, out message);
        }

        public static bool CreateLineItem(string candidate, Order order)
        {
            return order.AddNewOrderLine(candidate);
        }

        public static bool CreateNewCustomer(string newCustomerName, int id, out string message)
        {
            // Check newCustomerName
            if (InputValidation.IsValidCustomerName(newCustomerName, out validCheckerResponse))
            {
                string[] names = newCustomerName.Split(' ');

                // If Valid name
                //Customer newCustomer = new Customer(names[0], names[1], id);
               // Customer.Customers.Add(newCustomer);
                //validCheckerResponse = $"Success! New Customer [{newCustomer.Name}] with ID [{newCustomer.ID}] has been added.";
                message = validCheckerResponse;
                return true;
            }
            // If invalid name
            message = validCheckerResponse;
            return false;
        }

        public static bool FindCustomerByName(string candidate, out string message, out Customer foundCustomer)
        {
            Customer c = null;
         
            if (InputValidation.IsValidCustomerName(candidate, out validCheckerResponse))
            {
                string results = "Found: ";
                foundCustomer = Customer.FindCustomerByName(candidate);
                results += foundCustomer;
                if (results == "Found: ")
                {
                    results += "\nNo Records Found.";
                }

                message = results;
                return true;
            }
            foundCustomer = c;
            message = validCheckerResponse;
            return false;
        }
    }
}
