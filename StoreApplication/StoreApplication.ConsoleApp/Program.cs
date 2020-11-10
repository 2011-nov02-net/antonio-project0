using StoreApplication.Library;
using System;

namespace StoreApplication.ConsoleApp
{
    class Program
    {
        public static string menu = "Welcome to your Book Store Management System:\n" +
                "[a]add a new customer" + "\n[sc]search customers by name" + "\n[ddo]display details of an order"
                + "\n[dhl]display all order history of a store location" + "[dhc]display all order history of a customer"
                + "\n[s] Save Changes" + "\n[help] Show Menu" + "\n[q] Quit Application";
        /// <summary>
        /// While the user is within the application and has not exited.
        /// Present to them the options they have to choose from in terms of functionality.
        /// </summary>
        static void Main(string[] args)
        {
            // Create StoreManager
            StoreManager storeManager = new StoreManager();

            InputValidation iv = new InputValidation();

            // Load data from file

            // Begin the app loop to collect input
            string input = "";
            string response = "";

            // Display Options
            Console.WriteLine(menu);
            while (input != "q")
            {
                // Collect Input
                input = Console.ReadLine();
                if (iv.IsValidMainMenuSelection(input, out response))
                {
                    switch (input)
                    {
                        case "a":
                            Console.WriteLine("You Have selected [Add New Customer].");
                            break;
                        case "sc":
                            Console.WriteLine("You Have selected [Search By Customer Name].");
                            break;
                        case "ddo":
                            Console.WriteLine("You Have selected [Display Details of an Order].");
                            break;
                        case "dhl":
                            Console.WriteLine("You Have selected [Display Order History of Location].");
                            break;
                        case "dhc":
                            Console.WriteLine("You Have selected [Display Order History of Customer].");
                            break;
                        case "s":
                            Console.WriteLine("You Have selected [Save Changes].");
                            break;
                        case "help":
                            Console.WriteLine(menu);
                            break;
                        case "q":
                            Console.WriteLine("You Have selected [Quit].");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(response);
                }
            }

            // Save data to file
        }
    }
}
