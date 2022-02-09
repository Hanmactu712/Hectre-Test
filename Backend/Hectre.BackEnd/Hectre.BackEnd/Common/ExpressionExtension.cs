using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hectre.BackEnd.Common
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> firstException,
            Expression<Func<T, bool>> secondException)
        {
            ParameterExpression param = firstException.Parameters[0];
            if (ReferenceEquals(param, secondException.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(firstException.Body, secondException.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    firstException.Body,
                    Expression.Invoke(secondException, param)), param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> firstException,
            Expression<Func<T, bool>> secondException)
        {
            ParameterExpression param = firstException.Parameters[0];
            if (ReferenceEquals(param, secondException.Parameters[0]))
            {
                // simple version
                return Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(firstException.Body, secondException.Body), param);
            }
            // otherwise, keep expr1 "as is" and invoke expr2
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    firstException.Body,
                    Expression.Invoke(secondException, param)), param);
        }

        public static Expression<Func<TModel, object>> GetSortExpressionByName<TModel>(string property)
        {
            Expression<Func<TModel, object>> expression = null;

            //STEP 1: Verify the property is valid
            var searchProperty = typeof(TModel).GetProperty(property);

            if (searchProperty == null)
                return null;

            if (!searchProperty.PropertyType.IsValueType &&
                !searchProperty.PropertyType.IsPrimitive &&
                (!string.IsNullOrEmpty(searchProperty.PropertyType.Namespace) &&
                 !searchProperty.PropertyType.Namespace.StartsWith("System")) &&
                !searchProperty.PropertyType.IsEnum)
                return null;

            if (searchProperty.GetMethod == null ||
                !searchProperty.GetMethod.IsPublic)
                return null;

            //STEP 2: Create the OrderBy property selector
            var parameter = Expression.Parameter(typeof(TModel), "o");

            var paramExpression = Expression.Property(parameter, property);

            if (searchProperty.PropertyType.IsValueType)
            {
                var convertedExpression = Expression.Convert(paramExpression, typeof(object));

                expression = Expression.Lambda<Func<TModel, object>>(convertedExpression, parameter);
            }
            else
            {
                expression = Expression.Lambda<Func<TModel, object>>(paramExpression, parameter);
            }

            return expression;
        }

        public static IQueryable<TModel> OrderBy<TModel>(this IQueryable<TModel> source, string property)
        {
            Expression<Func<TModel, object>> expression = GetSortExpressionByName<TModel>(property);

            if (expression == null) return source;

            var searchProperty = typeof(TModel).GetProperty(property);

            Expression queryExpr = source.Expression;
            if (searchProperty != null)
                queryExpr = Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new Type[]
                    {
                        source.ElementType,
                        searchProperty.PropertyType
                    },
                    queryExpr,
                    expression);

            return source.Provider.CreateQuery<TModel>(queryExpr);

        }

        public static IEnumerable<TModel> OrderBy<TModel>(this IEnumerable<TModel> source, string property)
        {
            Expression<Func<TModel, object>> expression = GetSortExpressionByName<TModel>(property);

            return expression == null ? source : source.OrderBy(expression.Compile());
        }

        public static IQueryable<TModel> OrderByDescending<TModel>(this IQueryable<TModel> source, string property)
        {
            Expression<Func<TModel, object>> expression = GetSortExpressionByName<TModel>(property);

            if (expression == null) return source;

            var searchProperty = typeof(TModel).GetProperty(property);

            Expression queryExpr = source.Expression;
            if (searchProperty != null)
                queryExpr = Expression.Call(
                    typeof(Queryable),
                    "OrderByDescending",
                    new Type[]
                    {
                        source.ElementType,
                        searchProperty.PropertyType
                    },
                    queryExpr,
                    expression);

            return source.Provider.CreateQuery<TModel>(queryExpr);
        }

        public static IEnumerable<TModel> OrderByDescending<TModel>(this IEnumerable<TModel> source, string property)
        {
            Expression<Func<TModel, object>> expression = GetSortExpressionByName<TModel>(property);

            return expression == null ? source : source.OrderByDescending(expression.Compile());
        }
    }
}
