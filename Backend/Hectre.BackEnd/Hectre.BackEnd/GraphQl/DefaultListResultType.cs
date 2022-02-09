using GraphQL.Types;
using System.Collections.Generic;

namespace Hectre.BackEnd.GraphQl
{
    public class DefaultListResultType<TData, TType> : ObjectGraphType<DefaultListResult<TData>> where TType : ObjectGraphType<TData>
    {
        public DefaultListResultType()
        {
            Name = $"List{typeof(TType).Name}";
            Field<ListGraphType<TType>>("data", resolve: context => context.Source.Data);
            Field<IntGraphType>("total", resolve: context => context.Source.Total);
            Field<StringGraphType>("code", resolve: context => context.Source.Code);
            Field<StringGraphType>("message", resolve: context => context.Source.Message);
        }
    }

    public class DefaultListResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
