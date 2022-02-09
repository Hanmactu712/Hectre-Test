using Ardalis.Specification;
using Hectre.BackEnd.Models;
using System;
using System.Linq.Expressions;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// Generic class for entity model specification which determine the query condition, sort condition & pagination condition to listing data
    /// </summary>
    /// <typeparam name="TModel">entity model which derived from BaseEntity class</typeparam>
    public sealed class EntitySpecs<TModel> : Specification<TModel> where TModel : BaseEntity
    {
        public EntitySpecs(Expression<Func<TModel, bool>> whereExpression,
            Expression<Func<TModel, object>> sortExpression = null, string sortDirection = "asc", int start = 0, int limit = -1)
        {
            if (whereExpression == null)
            {
                if (sortExpression != null)
                {
                    if (!string.IsNullOrEmpty(sortDirection) && sortDirection.ToLower().Equals("asc",
                        StringComparison.CurrentCultureIgnoreCase))
                        Query.OrderBy(sortExpression);
                    else
                        Query.OrderByDescending(sortExpression);
                }
            }
            else
            {

                if (sortExpression != null)
                {
                    if (!string.IsNullOrEmpty(sortDirection) && sortDirection.ToLower().Equals("asc",
                        StringComparison.CurrentCultureIgnoreCase))
                        Query.Where(whereExpression).OrderBy(sortExpression);
                    else
                        Query.Where(whereExpression).OrderByDescending(sortExpression);
                }
                else
                {
                    Query.Where(whereExpression);
                }
            }

            var startItem = start <= 0 ? 0 : start;

            if (limit <= 0) return;

            if (sortExpression != null)
                Query.Skip(startItem).Take(limit);
            else
                Query.OrderBy(e => e.Id).Skip(startItem).Take(limit);
        }
    }
}
