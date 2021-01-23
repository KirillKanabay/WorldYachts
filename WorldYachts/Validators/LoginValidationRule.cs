using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class LoginValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string login = (value ?? "").ToString();
            Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d._]{0,32}$");
            if (loginRegex.IsMatch(login))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Логин может состоять из латинских букв, цифр, знака подчеркивания (' _ ') и точки (' . '). " +
                                                   "Длина не более 32 символа.");
            }
        }
    }
}
