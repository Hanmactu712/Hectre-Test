using GraphQL.Types;
using Hectre.BackEnd.Common;

namespace Hectre.BackEnd.GraphQl.Chemical
{
    public class ChemicalMutation
    {
        public static void InitiateField(ObjectGraphType root)
        {
            root.CustomField<DefaultSingleResultType<Models.Chemical, ChemicalType>, CreateChemicalInputType,
                Models.Chemical>(Constants.GraphQuery.ChemicalMutation.CreateChemical, (args, dataContext) => new ChemicalResolver().AddChemical(args, dataContext));
        }
    }
}
