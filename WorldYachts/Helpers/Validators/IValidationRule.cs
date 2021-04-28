namespace WorldYachts.Helpers.Validators
{
    public interface IValidationRule
    {
        object Value { get; }
        public string Validate();
    }
}
