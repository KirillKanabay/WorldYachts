using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class NotEmptyFieldValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return (value ?? "").ToString().Length > 0
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Поле не должно быть пустым");
        }
    }
}
