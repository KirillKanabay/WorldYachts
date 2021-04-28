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
    class EmailValidationRule:IValidationRule
    { 
        public object Value { get; }
        public EmailValidationRule(object value)
        {
            Value = value;
        }
        public string Validate()
        {
            string validationError = null;
            
            string email = (Value ?? "").ToString();
            try
            {
               var address = new MailAddress(email).Address;
            }
            catch (Exception)
            {
                validationError = "Неправильный формат Email";
            }

            return validationError;
        }
    }
}
