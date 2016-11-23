using System;
using System.Linq.Expressions;
using FundManager.Interfaces.Validation;

namespace FundManager.Impl.Validation
{
    internal class ValidationMap<T>
    {
        private Expression<Func<T, object>> _expression;

        public ValidationMap(Expression<Func<T, object>> expression, IValidation validation)
        {
            _expression = expression;
            PropertyDelegate = _expression.Compile();
            Validation = validation;
            PropertyName = GetPropertyName(_expression);
        }
        public Func<T, object> PropertyDelegate { get; set; }
        public IValidation Validation { get; }
        public string PropertyName { get; }

        public IValidationResult Validate(T value)
        {
            var res = Validation.Validate(PropertyDelegate(value));
            return res;
        }

        private string GetPropertyName<T1>(Expression<Func<T, T1>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            MemberExpression memberExpression = body as MemberExpression;
            if (memberExpression == null)
            {
                memberExpression = (MemberExpression)((UnaryExpression)body).Operand;
            }

            return memberExpression.Member.Name;
        }
    }
}