using System;

namespace WorldYachts.Helpers.Validators
{
    class YearsOldValidationRule:IValidationRule
    {
        public object Value { get; }

        public YearsOldValidationRule(object value)
        {
            Value = value;
        }
        public string Validate()
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
            return DateTime.Compare(DateTime.Now.AddYears(-18),dt) >= 0 ? null : "Вам должно быть не менее 18 лет.";
        }
    }
}
