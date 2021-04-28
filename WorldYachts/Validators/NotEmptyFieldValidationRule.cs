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
        public string Validate()
        {
            return string.IsNullOrWhiteSpace((Value ?? "").ToString())
                ? "Поле не должно быть пустым"
                : null;
        }
    }
}
