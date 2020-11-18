using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.Library.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace StoreApplication.ConsoleApp
{
    class Program
    {
        public static string menu = "Welcome to your Book Store Management System:\n"
                + "[p]\tPlace an order for a customer\n[a]\tAdd a new Customer" + "\n[sc]\tSearch for a customer by name" + "\n[ddo]\tDisplay details of an order"
                + "\n[dhl]\tDisplay all order history of a store location" + "\n[dhc]\tDisplay all order history of a customer"
                + "\n[help]\tShow Menu" + "\n[q]\tQuit Application";
        /// <summary>
        /// While the user is within the application and has not exited.
        /// Present to them the options they have to choose from in terms of functionality.
        /// </summary>
        static void Main(string[] args)
        {
            // Create our dependencies
            using var dependencies = new Dependencies();

            // Create the reference to the store repository to be used in each of our menu selections
            IStoreRepository storeRepository = dependencies.CreateStoreRepository();

            // Run the menu selection loop
            RunMenuSelection(storeRepository);
        }

        /// <summary>
        /// The main purpose of this function is to run a while loop until the user is done with all of their interactions.
        /// It has checks to make sure that input for a menu selection is valid as well as checks for input from the user in each selection menu option
        /// </summary>
        /// <param name="storeRepository"> This is the reference to the StoreRepository to call the functions to be handled by the db</param>
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
                if (InputValidation.IsValidMainMenuSelection(menuOption, out response))
                {
                    string input = "";
                    switch (menuOption)
                    {
                        case "p":
                            Console.WriteLine("You have selected [Place an order for an existing customer]. Please enter the customer ID you want to place the order for:");
                            string number = Console.ReadLine();

                            // Get input until a valid name is given
                            while (!InputValidation.IsValidNumber(number))
                            {
                                Console.WriteLine(response);
                                number = Console.ReadLine();
                            }
                            int id = Int32.Parse(number);

                            // Once a valid name is given try to assign it to a new object
                            var existingCustomer = new Customer();

                            // Get a customer from the database
                            Console.WriteLine("Searching db for customer...");
                            existingCustomer = storeRepository.GetCustomerWithLocationAndInventory(id);

                            // if an exception was thrown then end this switch
                            if (existingCustomer == null)
                            {
                                Console.WriteLine("Customer does not exist. Please pick a menu option to begin again.");
                                break;
                            }

                            // Continue though if the customer did exist
                            Console.WriteLine($"Selected Customer: {existingCustomer}");

                            // Begin the prompt to get each order line.
                            Console.WriteLine("Please enter the product ISBN and quantity separated by a comma.\nType [done] when order is complete.");
                            string orderlineItem;

                            // Create the new order to add the orderlines and to use the library methods to ensure that it is possible to create the order.
                            var order = new Order();
                            orderlineItem = Console.ReadLine();

                            // This function will fill the static list of the entire catalog to be used by any function or object
                            storeRepository.FillBookLibrary();

                            bool success = true;
                            // Collect input untill there is a failure
                            while (orderlineItem != "done")
                            {
                                // If a failure does occur let the user know they must begin transaction from the beginning
                                if (!order.AddNewOrderLine(orderlineItem))
                                {
                                    Console.WriteLine("Either the ISBN does not exist in our library or your quantity was not a valid number!"
                                        + "\nPlease start process again from the main menu.");
                                    success = !success;
                                    break;
                                }
                                // Let them know if it a success and then continue collecting input until it fails
                                Console.WriteLine("Successfully added to the order.");
                                orderlineItem = Console.ReadLine();
                            }
                            if (!success)
                            {
                                break;
                            }

                            // Now that we've collected and created all the objects we assign them to the order to be mapped later
                            order.CustomerPlaced = existingCustomer;
                            order.LocationPlaced = existingCustomer.MyStoreLocation;

                            // Before that though we want to make sure we can place the order with the given locations inventory
                            if (!order.LocationPlaced.AttemptOrderAtLocation(order, out response))
                            {
                                // If it failed, let the user know why and then end the switch
                                Console.WriteLine(response);
                                break;
                            }
                            Console.WriteLine(response);
                            // If it is possible then we send the data over to the db so that it is saved
                            Console.WriteLine("Placing order...");
                            storeRepository.PlaceAnOrderForACustomer(order);
                            Console.WriteLine("Order Placed!");
                            break;
                        case "a":
                            Console.WriteLine("You Have selected [Add New Customer]." + "\nPlease enter the name of the first and last name of the customer separated by a space:");
                            input = Console.ReadLine();

                            // Collect input until a valid name is given
                            while (!InputValidation.IsValidCustomerName(input, out response))
                            {
                                Console.WriteLine(response);
                                input = Console.ReadLine();
                            }

                            // attempt to attach the name to a model object if it fails let the user know that it failed and why
                            string[] newCustomerNames = input.Split(' ');
                            var newCustomer = new Customer();
                            try
                            {
                                newCustomer.FirstName = newCustomerNames[0];
                                newCustomer.LastName = newCustomerNames[1];
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message
                                        + "\nPlease start process again from the main menu.");
                                break;
                            }

                            // Add the new customer to the database
                            storeRepository.AddACustomer(newCustomer);

                            // let the user know that it was successful
                            Console.WriteLine($"Customer {newCustomer.FirstName} {newCustomer.LastName} was added with the default location.");
                            break;
                        case "sc":
                            Console.WriteLine("You Have selected [Search By Customer Name]." + "\nPlease enter the full name of the customer:");

                            input = Console.ReadLine();

                            // Collect input until a valid name is given
                            while (!InputValidation.IsValidCustomerName(input, out response))
                            {
                                Console.WriteLine(response);
                                input = Console.ReadLine();
                            }

                            string[] candidate = input.Split(' ');
                            var a = new Customer();

                            // Attempt to take the name and split it into two the customer
                            try
                            {
                                a.FirstName = candidate[0];
                                a.LastName = candidate[1];
                            }

                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            // If the strings are valid then we attempt to find the customer
                            List<Customer> foundCustomer = storeRepository.FindCustomerByName(candidate);

                            // Let the user know what we found
                            if(foundCustomer.Count() == 0)
                            {
                                Console.WriteLine($"No customer by the name of {candidate[0]} {candidate[1]} exists in the database.");
                            }
                            else
                            {
                                foreach(Customer c in foundCustomer)
                                {
                                    Console.WriteLine(c);
                                }
                            }
                            break;
                        case "ddo":
                            Console.WriteLine("You Have selected [Display Details of an Order]." + "\nPlease enter the order number:");
                            input = Console.ReadLine();

                            // Do Some input validation
                            while (!InputValidation.IsValidNumber(input))
                            {
                                Console.WriteLine("Input was not a number, please input a number.");
                                input = Console.ReadLine();
                            }

                            Console.WriteLine(storeRepository.GetDetailsForOrder(Int32.Parse(input)));
                            break;
                        case "dhl":
                            Console.WriteLine("You Have selected [Display Order History of Location]." + "\nPlease enter the location ID:");
                            input = Console.ReadLine();

                            // do some input validation
                            while (!InputValidation.IsValidNumber(input))
                            {
                                Console.WriteLine("Input was not a number, please input a number.");
                                input = Console.ReadLine();
                            }

                            Console.WriteLine(storeRepository.GetOrderHistoryByLocationID(Int32.Parse(input)));
                            break;
                        case "dhc":
                            Console.WriteLine("You Have selected [Display Order History of Customer]. Please enter the customer ID:");
                            input = Console.ReadLine();

                            // do some input validation until a valid two string is given
                            while (!InputValidation.IsValidNumber(input))
                            {
                                Console.WriteLine(response);
                                input = Console.ReadLine();
                            }

                            // Print what was found
                            Console.WriteLine(storeRepository.GetOrderHistoryByCustomer(Int32.Parse(input)));
                            break;
                        case "help":
                            // Print the menu options again for those that need it
                            Console.WriteLine(menu);
                            break;
                        case "q":
                            // Exit the program.
                            Console.WriteLine("You Have selected [Quit].");
                            break;
                    }
                }
                else { Console.WriteLine(response); }
            }
        }
    }
}
