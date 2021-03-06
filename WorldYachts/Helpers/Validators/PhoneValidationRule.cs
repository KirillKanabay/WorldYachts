﻿using System.Text.RegularExpressions;

namespace WorldYachts.Helpers.Validators
{
    class PhoneValidationRule:IValidationRule
    {
        public object Value { get; }

        public PhoneValidationRule(object value)
        {
            Value = value;
        }
        public string Validate()
        {
            string validationError = null;

            string phone = (Value ?? "").ToString();
            Regex phoneRegex = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
            if (!phoneRegex.IsMatch(phone))
            {
                validationError = "Неверный формат номера";
            }

            return validationError;
        }
    }
}
