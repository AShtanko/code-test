using System.Collections.Generic;

namespace FundManager.Interfaces.Validation
{
    public interface IValidator<T>
    {
        IList<IValidationResult> Validate(string keyType, string propertyName, T value);
        IList<IValidationResult> ValidateAll(string keyType, T value);
    }
}