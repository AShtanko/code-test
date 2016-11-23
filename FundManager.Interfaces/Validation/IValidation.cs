namespace FundManager.Interfaces.Validation
{
    public interface IValidation
    {
        int? ErrorId { get; }

        IValidationResult Validate(object value);
    }
}