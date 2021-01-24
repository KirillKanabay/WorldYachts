using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    static class SafePasswordValidationRule
    {
        public static void Validate(object value, ref string validationError)
        {
            string password = (value ?? "").ToString();
            //Безопасный пароль
            //1 строчная
            //1 заглавная
            //1 цифра
            //Не менее 8 символов
            Regex safePasswordRegex = new Regex(@"^(?=.*[(A-Z)|(А-Я)])(?=.*[0-9])(?=.*[(a-z)|(a-я)]).{8,}$");
            if (!safePasswordRegex.IsMatch(password))
            {
                validationError = "Слабый пароль. Пароль должен содержать как минимум " +
                                  "1 строчную и 1 заглавную букву, 1 цифру. Длина пароля минимум 8 символов";
            }
        }
    }
}
