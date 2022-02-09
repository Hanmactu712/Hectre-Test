using GraphQL.Types;
using System;

namespace Hectre.BackEnd.GraphQl
{
    public class DefaultDetailInputType : InputObjectGraphType<DefaultDetailInput>
    {
        public DefaultDetailInputType()
        {
            Field(e => e.Id, type: typeof(IdGraphType));
        }
    }

    public class DefaultDetailInput
    {
        public string Id { get; set; }
    }
}
