using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class YearsOldValidationRule:IValidationRule
    {
        public object Value { get; }

        public YearsOldValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            DateTime dt;
            if (Value == null)
            {
                dt = DateTime.Now;
            }
            else
            {
                dt = (DateTime) Value;
            }
            validationError = DateTime.Compare(DateTime.Now.AddYears(-18),dt) >= 0 ? validationError : "Вам должно быть не менее 18 лет.";
        }
    }
}
