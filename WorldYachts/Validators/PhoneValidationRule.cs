using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class PhoneValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phone = (value ?? "").ToString();
            Regex phoneRegex = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
            if (phoneRegex.IsMatch(phone))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Неверный формат номера");
            }
        }
    }
}
