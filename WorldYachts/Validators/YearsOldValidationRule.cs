using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class YearsOldValidationRule
    {
        public static void Validate(object value, ref string validationError)
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
            validationError = DateTime.Compare(DateTime.Now.AddYears(-18),dt) >= 0 ? validationError : "Вам должно быть не менее 18 лет.";
        }
    }
}
