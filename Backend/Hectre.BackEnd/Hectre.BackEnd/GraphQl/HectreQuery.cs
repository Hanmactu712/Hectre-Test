using GraphQL;
using GraphQL.Types;
using Hectre.BackEnd.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Hectre.BackEnd.GraphQl.Chemical;

namespace Hectre.BackEnd.GraphQl
{
    public sealed class HectreQuery : ObjectGraphType
    {
        public HectreQuery()
        {
            Name = "Query";
            ChemicalQuery.InitiateField(this);
        }


    }

    public static class ObjectGraphTypeExtension
    {
        public static void CustomField<TType, TArgType, TArg>(this ObjectGraphType obj, string name,
            Func<TArg, DataContext, object> resolver, string description = "")
            where TArgType : InputObjectGraphType<TArg> where TType : IGraphType
        {
            obj.Field<TType>().Name(name)
                .Description(description)
                .Argument<TArgType>("input")
                .Resolve(context =>
                {
                    var args = context.GetArgument<TArg>("input");

                    using var scope = context.RequestServices.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var dataContext = scopedServices.GetRequiredService<DataContext>();
                    return resolver(args, dataContext);
                });
        }
    }
}
