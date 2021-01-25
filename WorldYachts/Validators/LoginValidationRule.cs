using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class LoginValidationRule:IValidationRule
    {
        public object Value { get; }

        public LoginValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            string login = (Value ?? "").ToString();
            Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d._]{0,32}$");
            if (!loginRegex.IsMatch(login))
            {
                validationError = "Логин может состоять из латинских букв, цифр, знака подчеркивания (' _ ') и точки (' . '). " +
                                  "Длина не более 32 символа.";
            }
        }
    }
}
