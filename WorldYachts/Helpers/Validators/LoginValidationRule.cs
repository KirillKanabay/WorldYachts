using System.Text.RegularExpressions;


namespace WorldYachts.Helpers.Validators
{
    class LoginValidationRule:IValidationRule
    {
        public object Value { get; }

        public LoginValidationRule(object value)
        {
            Value = value;
        }
        public string Validate()
        {
            string validationError = null;
            
            string login = (Value ?? "").ToString();
            Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d._]{0,32}$");
            if (!loginRegex.IsMatch(login))
            {
                validationError = "Логин может состоять из латинских букв, цифр, знака подчеркивания (' _ ') и точки (' . '). " +
                                  "Длина не более 32 символа.";
            }

            return validationError;
        }
    }
}
