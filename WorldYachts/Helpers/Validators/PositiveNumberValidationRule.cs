namespace WorldYachts.Helpers.Validators
{
    class PositiveNumberValidationRule:IValidationRule
    {
        public object Value { get; }

        public PositiveNumberValidationRule(object value)
        {
            Value = value;
        }
        public string Validate()
        {
            string validationError = null;
            if (Value is decimal decimalValue && decimalValue < 0)
            {
                validationError = "Число не может быть отрицательным";
            }
            if(Value is double doubleValue && doubleValue < 0)
            {
                validationError = "Число не может быть отрицательным";
            }

            return validationError;
        }
    }
}
