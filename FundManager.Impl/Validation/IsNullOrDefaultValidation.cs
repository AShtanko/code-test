using System;
using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    public class IsNullOrDefaultValidation : ValidationBase
    {
        private readonly ValidationType _validationType;

        public IsNullOrDefaultValidation(int? errorId, ValidationType validationType) : base(errorId)
        {
            _validationType = validationType;
        }

        public override IValidationResult Validate(object value)
        {
            if (value == null || value.Equals(GetDefault(value.GetType())))
                return new ValidationResult("test null", _validationType);

            return null;
        }

        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return defaultValue;
            }

            return null;
        }
    }
}