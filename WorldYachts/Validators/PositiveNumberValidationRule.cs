using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Validators
{
    class PositiveNumberValidationRule:IValidationRule
    {
        public object Value { get; }

        public PositiveNumberValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            if (Value is decimal && (decimal)Value < 0)
            {
                validationError = "Число не может быть отрицательным";
            }
            if(Value is double && (double)Value < 0)
            {
                validationError = "Число не может быть отрицательным";
            }
        }
    }
}
