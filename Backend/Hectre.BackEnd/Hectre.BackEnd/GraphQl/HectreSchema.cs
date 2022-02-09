using GraphQL.Instrumentation;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hectre.BackEnd.GraphQl
{
    public class HectreSchema : GraphQL.Types.Schema, ISchema
    {
        public HectreSchema(IServiceProvider provider, InstrumentFieldsMiddleware middleware)
        {
            Query = provider.GetRequiredService<HectreQuery>();
            Mutation = provider.GetRequiredService<HectreMutation>();
        }
    }
}
