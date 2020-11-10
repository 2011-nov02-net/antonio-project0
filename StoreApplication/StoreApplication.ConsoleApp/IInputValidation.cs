using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IInputValidation
    {
        public bool IsValidMainMenuSelection(string candidate, out string message);
        bool IsValidString(string candidate);
        bool IsValidNumber(string candidate);
    }
}
