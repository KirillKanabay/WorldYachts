using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class YearsOldValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime dt;
            if (value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                dt = (DateTime) value;
            }
            if (DateTime.Compare(DateTime.Now.AddYears(-18),dt) >= 0)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Вам должно быть не менее 18 лет.");
            }
        }
    }
}
