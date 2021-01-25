using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace WorldYachts.Validators
{
    class NotEmptyFieldValidationRule:IValidationRule
    {
        public object Value { get; }

        public NotEmptyFieldValidationRule(object value)
        {
            Value = value;
        }
        public void Validate(ref string validationError)
        {
            validationError = (Value ?? "").ToString().Trim().Length > 0
                ? validationError
                : "Поле не должно быть пустым";
        }
    }
}
