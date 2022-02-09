using GraphQL.Types;
using Hectre.BackEnd.GraphQl.Chemical;

namespace Hectre.BackEnd.GraphQl
{
    public class HectreMutation : ObjectGraphType
    {
        public HectreMutation()
        {
            Name = "Mutation";

            ChemicalMutation.InitiateField(this);
        }
    }
}
