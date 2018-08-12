using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Assets.Scripts.MVVM
{
    public static class PropertyExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var body = property.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Expression is not a property", nameof(property));
            }

            var propertyInfo = body.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expression is not a property", nameof(property));
            }

            return propertyInfo;
        }
    }
}
