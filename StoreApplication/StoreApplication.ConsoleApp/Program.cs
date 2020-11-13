using StoreApplication.Library;
using System;
using System.Text.RegularExpressions;


namespace StoreApplication.ConsoleApp
{
    class Program
    {
        public static string menu = "Welcome to your Book Store Management System:\n" +
                "[a]\tAdd a new Customer" + "\n[sc]\tSearch for a customer by name" + "\n[ddo]\tDisplay details of an order"
                + "\n[dhl]\tDisplay all order history of a store location" + "\n[dhc]\tDisplay all order history of a customer"
                + "\n[s]\tSave Changes" + "\n[help]\tShow Menu" + "\n[q]\tQuit Application";
        /// <summary>
        /// While the user is within the application and has not exited.
        /// Present to them the options they have to choose from in terms of functionality.
        /// </summary>
        static void Main(string[] args)
        {
            // Load data from file

            // Begin the app loop to collect input
            string menuOption = "";
            string response;

            // Display Options
            Console.WriteLine(menu);
            while (menuOption != "q")
            {
                // Collect Input
                menuOption = Console.ReadLine();
                if (IsValidMainMenuSelection(menuOption, out response))
                {
                    string input = "";
                    switch (menuOption)
                    {
                        case "a":
                            Console.WriteLine("You Have selected [Add New Customer]."+"\nPlease enter the name of the first and last name of the customer separated by a space:");
                            input = Console.ReadLine();
                            while (!StoreManager.CreateNewCustomer(input, out response))
                            {
                                Console.WriteLine(response);
                                input = Console.ReadLine();
                            }
                            Console.WriteLine(response);
                            break;
                        case "sc":
                            Console.WriteLine("You Have selected [Search By Customer Name]."+"\nPlease enter the full name of the customer:");
                            input = Console.ReadLine();
                            while(!StoreManager.FindCustomerByName(input, out response))
                            {
                                Console.WriteLine(response);
                                input = Console.ReadLine();
                            }
                            Console.WriteLine(response);
                            break;
                        case "ddo":
                            Console.WriteLine("You Have selected [Display Details of an Order]."+"\nPlease enter the order number:");
                            break;
                        case "dhl":
                            Console.WriteLine("You Have selected [Display Order History of Location]."+"\nPlease enter the location ID:");
                            break;
                        case "dhc":
                            Console.WriteLine("You Have selected [Display Order History of Customer]."+"Please enter the customer name:");
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

        private static readonly string[] _mainMenuSelections = { "a", "sc", "ddo", "dhl", "dhc", "s", "q", "help" };
        private static Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$");

        public static bool IsValidMainMenuSelection(string candidate, out string message)
        {
            if (!IsValidString(candidate))
            {
                message = "Please use Alphanumeric numbers only!";
                return false;
            }
            foreach (var option in _mainMenuSelections)
            {
                if (candidate.ToLower() == option)
                {
                    message = "";
                    return true;
                }
            }
            message = "Input was not a menu option. Please enter a menu option. Enter [help] to see menu options again.";
            return false;
        }

        public static bool IsValidNumber(string candidate)
        {
            int i;
            return int.TryParse(candidate, out i);
        }

        public static bool IsValidString(string candidate)
        {
            return alphanumeric.IsMatch(candidate);
        }
    }
}
