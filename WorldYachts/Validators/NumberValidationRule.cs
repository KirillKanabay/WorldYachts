using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WorldYachts.Validators
{
    class NumberValidationRule:IValidationRule
    {
        public object Value { get; }

        public NumberValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            Regex doubleRegex = new Regex(@"^[0-9]*[,]?[0-9]+$");
            if (!doubleRegex.IsMatch((Value ?? "").ToString()))
            {
                validationError = "Поле должно содержать число";
            }
        }
    }
}
