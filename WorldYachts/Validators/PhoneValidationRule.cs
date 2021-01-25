using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class PhoneValidationRule:IValidationRule
    {
        public object Value { get; }

        public PhoneValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            string phone = (Value ?? "").ToString();
            Regex phoneRegex = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
            if (!phoneRegex.IsMatch(phone))
            {
                validationError = "Неверный формат номера";
            }
        }
    }
}
