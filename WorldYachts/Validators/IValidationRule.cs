using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Validators
{
    interface IValidationRule
    {
        object Value { get; }
        public void Validate(ref string validationError);
    }
}
