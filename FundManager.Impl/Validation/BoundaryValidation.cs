using System;
using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    public class BoundaryValidation : ValidationBase, IBoundaryValidation
    {
        private readonly ValidationType _validationType;

        public BoundaryValidation(decimal? lowerLimit, decimal? upperLimit, int? errorId, ValidationType validationType)
            : base(errorId)
        {
            _validationType = validationType;
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
        }

        public decimal? LowerLimit { get; }
        public decimal? UpperLimit { get; }

        public override IValidationResult Validate(object value)
        {
            if (value == null) return null;

            decimal parsedValue;
            var canParse = Decimal.TryParse(value.ToString(), out parsedValue);
            if (canParse == false)
                throw new Exception("incompatible validation value type");

            if (LowerLimit < parsedValue && parsedValue < UpperLimit)
                return null;

            return new ValidationResult("test", _validationType);
        }
    }
}