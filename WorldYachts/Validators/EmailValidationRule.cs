using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using WorldYachts.Annotations;

namespace WorldYachts.Validators
{
    static class EmailValidationRule
    {
        public static void Validate(object value, ref string validationError)
        {
            string email = (value ?? "").ToString();
            try
            {
               var address = new MailAddress(email).Address;
            }
            catch (Exception)
            {
                validationError = "Неправильный формат Email";
            }
        }
    }
}
