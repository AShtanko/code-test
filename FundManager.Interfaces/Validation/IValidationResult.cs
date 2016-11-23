namespace FundManager.Interfaces.Validation
{
    public enum ValidationType
    {
        Error = 1,
        Warning = 2,
        Adorner = 3
    }

    public interface IValidationResult
    {
        string ErrorMessage { get; }
        ValidationType ValidationType { get; }
    }
}