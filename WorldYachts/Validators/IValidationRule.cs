using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Validators
{
    interface IValidationRule
    {
        object Value { get; }
        public string Validate();
    }
}
