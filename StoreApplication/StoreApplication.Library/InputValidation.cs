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

        public static bool IsValidCustomerName(string candidate, out string message)
        {
            if(candidate.Split(' ').Length != 2)
            {
                message = "Please enter only a first and a last name with a single space between them. [FirstName] [LastName].";
                return false;
            }
            message = " ";
            return true;
        }
    }
}
