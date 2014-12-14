using System;
using System.Linq.Expressions;

namespace Bloghost.Logic.Validation
{
    public static class PropertyName
    {
        public static string For<TObject, TProperty>(this Expression<Func<TObject, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return GetPropertyName(expression.Body);
        }

        public static string For<TObject>(this Expression<Func<TObject, object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return GetPropertyName(expression.Body);
        }

        private static string GetPropertyName(Expression expression)
        {
            var propertyExpression = expression as MemberExpression;

            if (propertyExpression == null)
            {
                throw new ArgumentException("Bad expression", "expression");
            }

            return propertyExpression.Member.Name;
        }
    }
}
