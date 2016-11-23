using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    public class ValidationResult : IValidationResult
    {
        public ValidationResult(string validationMessage, ValidationType validationType)
        {
            ErrorMessage = validationMessage;
            ValidationType = validationType;
        }

        public string ErrorMessage { get; }

        public ValidationType ValidationType { get; }
    }
}