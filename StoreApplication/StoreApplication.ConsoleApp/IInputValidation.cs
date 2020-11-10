using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library
{
    interface IInputValidation
    {
        bool InputIsValidString();
        bool InputIsValidNumber();
    }
}
