using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    public abstract class ValidationBase : IValidation
    {
        protected ValidationBase(int? errorId)
        {
            ErrorId = errorId;
        }

        public int? ErrorId { get; }

        public abstract IValidationResult Validate(object value);
    }
}