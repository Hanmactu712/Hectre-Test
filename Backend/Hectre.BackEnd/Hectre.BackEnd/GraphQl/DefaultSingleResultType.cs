using GraphQL.Types;

namespace Hectre.BackEnd.GraphQl
{
    public class DefaultSingleResultType<TData, TType> : ObjectGraphType<DefaultSingleResult<TData>> where TType : ObjectGraphType<TData>
    {
        public DefaultSingleResultType()
        {
            Name = $"Single{typeof(TType).Name}";
            Field<TType>("data", resolve: context => context.Source.Data);
            Field<IntGraphType>("total", resolve: context => context.Source.Total);
            Field<StringGraphType>("code", resolve: context => context.Source.Code);
            Field<StringGraphType>("message", resolve: context => context.Source.Message);
        }
    }

    public class DefaultSingleResult<T>
    {
        public T Data { get; set; }
        public int Total { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
