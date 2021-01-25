using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Annotations;

namespace WorldYachts.Validators
{
    class Validation
    {
        private readonly IValidationRule[] _validationRules;
        public Validation(params IValidationRule[] validationRules)
        {
            _validationRules = validationRules;
        }

        public void Validate(ref string validationError)
        {
            validationError = String.Empty;
            foreach (var validationRule in _validationRules)
            {
                validationRule.Validate(ref validationError);
            }
        }
    }
}
