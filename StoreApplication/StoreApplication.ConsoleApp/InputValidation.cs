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

        public static bool IsValidWord(string candidate)
        {
            return letters.IsMatch(candidate);
        }

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
