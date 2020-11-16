using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.Library.Interfaces;
using System;
using System.Text.RegularExpressions;
using NLog;
using System.Xml.Serialization;
using System.Linq;

namespace StoreApplication.ConsoleApp
{
    class Program
    {

        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        public static string menu = "Welcome to your Book Store Management System:\n" 
                + "[p]\tPlace an order for a customer\n[a]\tAdd a new Customer" + "\n[sc]\tSearch for a customer by name" + "\n[ddo]\tDisplay details of an order"
                + "\n[dhl]\tDisplay all order history of a store location" + "\n[dhc]\tDisplay all order history of a customer"
                + "\n[s]\tSave Changes" + "\n[help]\tShow Menu" + "\n[q]\tQuit Application";
        /// <summary>
        /// While the user is within the application and has not exited.
        /// Present to them the options they have to choose from in terms of functionality.
        /// </summary>
        static void Main(string[] args)
        {
            // Load data
            using var dependencies = new Dependencies();
            XmlSerializer serializer = dependencies.CreateXmlSerializer();

            IStoreRepository storeRepository = dependencies.CreateStoreRepository();
            
            RunMenuSelection(storeRepository);
        }

        public static void DisplayAllLocations(IStoreRepository storeRepository)
        {

            var locations = storeRepository.GetAllLocations().ToList();
            foreach (Location i in locations)
            {
                Console.WriteLine($"ID: {i.ID}\tLocation Name: {i.LocationName}");
                foreach (Stock s in i.Inventory)
                {
                    Console.WriteLine($"\tISBN:{s.Book.ISBN}\tStock:{s.Quantity}");
                }
            }
        }

        private static readonly string[] _mainMenuSelections = { "p", "a", "sc", "ddo", "dhl", "dhc", "s", "q", "help" };
        private static Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$");

        public static void RunMenuSelection(IStoreRepository storeRepository)
        {
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
                        case "p":
                            Console.WriteLine("You have selected [Place an order for an existing customer]. Please enter the customer name you want to place the order for:");
                            string name = Console.ReadLine();

                            break;
                        case "a":
                            Console.WriteLine("You Have selected [Add New Customer]." + "\nPlease enter the name of the first and last name of the customer separated by a space:");
                            input = Console.ReadLine();

                            if(InputValidation.IsValidCustomerName(input, out response))
                            {
                                string[] names = input.Split(' ');
                                var newCustomer = new Customer();
                                try
                                {
                                    newCustomer.FirstName = names[0];
                                    newCustomer.LastName = names[1];
                                }
                                catch (ArgumentException ex)
                                {
                                    s_logger.Info(ex);
                                    Console.WriteLine(ex.Message);
                                }

                                storeRepository.AddACustomer(newCustomer);
                            }
                            break;
                        case "sc":
                            Console.WriteLine("You Have selected [Search By Customer Name]." + "\nPlease enter the full name of the customer:");

                            input = Console.ReadLine();
                            string[] candidate = input.Split(' ');
                            var a = new Customer();
                            try
                            {
                                a.FirstName = candidate[0];
                                a.LastName = candidate[1];
                            }
                            catch (ArgumentException ex)
                            {
                                s_logger.Info(ex);
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine(storeRepository.FindCustomerByName(candidate).ToString());
                            break;
                        case "ddo":
                            Console.WriteLine("You Have selected [Display Details of an Order]." + "\nPlease enter the order number:");
                            input = Console.ReadLine();

                            // Do Some input validation

                            Console.WriteLine(storeRepository.GetDetailsForOrder(Int32.Parse(input)));
                            break;
                        case "dhl":
                            Console.WriteLine("You Have selected [Display Order History of Location]." + "\nPlease enter the location ID:");
                            input = Console.ReadLine();

                            // do some input validation

                            Console.WriteLine(storeRepository.GetOrderHistoryByLocationID(Int32.Parse(input)));
                            break;
                        case "dhc":
                            Console.WriteLine("You Have selected [Display Order History of Customer]." + "Please enter the customer name:");
                            input = Console.ReadLine();
                            string[] s = input.Split(' ');
                            // do some input validation

                            Console.WriteLine(storeRepository.GetOrderHistoryByCustomer(s));
                            break;
                        case "s":
                            Console.WriteLine("You Have selected [Save Changes].");

                            storeRepository.Save();
                            break;
                        case "help":
                            Console.WriteLine(menu);
                            break;
                        case "q":
                            Console.WriteLine("You Have selected [Quit].");
                            break;
                    }
                }
            }
        }

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
