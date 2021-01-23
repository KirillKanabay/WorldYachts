using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class EmailValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = value.ToString() ?? "";
            try
            {
               var address = new MailAddress(email).Address;
               return ValidationResult.ValidResult;
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Неправильный формат Email");
            }
        }
    }
}
