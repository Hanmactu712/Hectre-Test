using System;
using System.Linq.Expressions;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// Generic class represent for sorting condition against an entity model
    /// </summary>
    /// <typeparam name="T">Entity model</typeparam>
    public class SortCondition<T>
    {
        public Expression<Func<T, object>> SortField { get; set; }
        public string SortDirection { get; set; }
    }
}
