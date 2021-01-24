using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    static class NotEmptyFieldValidationRule
    {
        public static void Validate(object value, ref string validationError)
        {
            validationError = (value ?? "").ToString().Trim().Length > 0
                ? validationError
                : "Поле не должно быть пустым";
        }
    }
}
