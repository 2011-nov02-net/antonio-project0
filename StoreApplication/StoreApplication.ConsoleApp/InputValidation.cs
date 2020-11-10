using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApplication.ConsoleApp
{
    class InputValidation : IInputValidation
    {
        private readonly string[] _mainMenuSelections = { "a", "sc", "ddo", "dhl", "dhc", "s", "q", "help"};
        Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$");

        public bool IsValidMainMenuSelection(string candidate, out string message)
        {
            if (!IsValidString(candidate)) {
                message = "Please use Alphanumeric numbers only!";
                return false ; }
            foreach (var option in _mainMenuSelections)
            {
                if (candidate.ToLower() == option)
                {
                    message = "";
                    return true;
                }
            }
            message = "Input was not a menu option. Please enter a menu option.";
            return false;
        }

        public bool IsValidNumber(string candidate)
        {
            int i;
            return int.TryParse(candidate, out i);
        }

        public bool IsValidString(string candidate)
        {
            return alphanumeric.IsMatch(candidate);
        }
    }
}
