using StoreApplication.Library;
using System;

namespace StoreApplication.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// While the user is within the application and has not exited.
        /// Present to them the options they have to choose from in terms of functionality.
        /// </summary>
        static void Main(string[] args)
        {
            // Create StoreManager
            StoreManager storeManager = new StoreManager();

            // Load data from file

            // Begin the app loop to collect input
            string input = "";
            while (input != "")
            {
                // Display Options

                // Collect Input

                // place orders to store locations for customers
                
                /*  
                    [a]add a new customer
                    [s]search customers by name
                    [ddo]display details of an order
                        get order number
                    [dhl]display all order history of a store location
                        get store location ID
                    [dhc]display all order history of a customer
                        get customer ID
                */
            }

            // Save data to file
        }
    }
}
