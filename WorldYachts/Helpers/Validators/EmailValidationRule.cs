using System;
using System.Net.Mail;

namespace WorldYachts.Helpers.Validators
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
