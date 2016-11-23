using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FundManager.Interfaces.Data;
using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    public class StockValidator : IValidator<IStock>
    {
        private readonly Dictionary<string, IList<ValidationMap<IStock>>> _validations
            = new Dictionary<string, IList<ValidationMap<IStock>>>();

        public IList<IValidationResult> Validate(string keyType, string propertyName, IStock value)
        {
            if (keyType == null || propertyName == null) return null;
            if (_validations.ContainsKey(keyType) == false) return null;

            var validationResult = new List<IValidationResult>();
            foreach (var validationMap in _validations[keyType].Where(p => p.PropertyName == propertyName))
            {
                var propertyValidationResult = validationMap.Validate(value);
                if (propertyValidationResult != null)
                    validationResult.Add(propertyValidationResult);
            }

            return validationResult;
        }

        public IList<IValidationResult> ValidateAll(string keyType, IStock value)
        {
            if (keyType == null) return null;
            if (_validations.ContainsKey(keyType) == false) return null;

            var validationResult = new List<IValidationResult>();
            foreach (var validationMap in _validations[keyType])
            {
                var propertyValidationResult = validationMap.Validate(value);
                if (propertyValidationResult != null)
                    validationResult.Add(propertyValidationResult);
            }

            return validationResult;
        }

        public StockValidator AddNewValidationRule(string keyType, Expression<Func<IStock, object>> expression,
            IValidation propertyValidation)
        {
            var validationMap = new ValidationMap<IStock>(expression, propertyValidation);
            if (_validations.ContainsKey(keyType))
                _validations[keyType].Add(validationMap);
            else
                _validations[keyType] = new List<ValidationMap<IStock>>() { validationMap };
            return this;
        }
    }
}