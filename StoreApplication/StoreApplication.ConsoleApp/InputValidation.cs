using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApplication.ConsoleApp
{
    public static class InputValidation
    {
        private static readonly string[] _mainMenuSelections = { "p", "a", "sc", "ddo", "dhl", "dhc", "q", "help" };
        private static Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$");
        private static Regex letters = new Regex("^[a-zA-Z]+$");

        /// <summary>
        /// Not only do we want to make sure that the two strings are characters but that they are words
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsValidCustomerName(string candidate, out string message)
        {
            string[] names = candidate.Split(' ');
            if (names.Length != 2)
            {
                message = "Please enter only a first and a last name with a single space between them. [FirstName] [LastName].";
                return false;
            }
            if (!IsValidWord(names[0]) || !IsValidWord(names[1]))
            {
                message = "Please use letters only for names.";
                return false;
            }
            message = " ";
            return true;
        }

        /// <summary>
        /// Checks if a given string is a word
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool IsValidWord(string candidate)
        {
            return letters.IsMatch(candidate);
        }

        /// <summary>
        /// Checks if a given menu selection is present in the list
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsValidMainMenuSelection(string candidate, out string message)
        {
            if (!IsValidString(candidate))
            {
                message = "Please use alphanumeric characters only!";
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

        /// <summary>
        /// Checks that the given string is a valid number
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string candidate)
        {
            int i;
            return int.TryParse(candidate, out i);
        }

        /// <summary>
        /// check that the given string only contains letters or numbers
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool IsValidString(string candidate)
        {
            return alphanumeric.IsMatch(candidate);
        }
    }
}
