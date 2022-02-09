using GraphQL.Types;
using Hectre.BackEnd.Common;

namespace Hectre.BackEnd.GraphQl.Chemical
{
    public class ChemicalQuery
    {
        public static void InitiateField(ObjectGraphType rootQuery)
        {
            rootQuery
                .CustomField<ListChemicalResultType, ChemicalListInputType,
                    ChemicalListInput>(Constants.GraphQuery.ChemicalQuery.GetList, (args, dataContext) => new ChemicalResolver().GetList(args, dataContext));

            //rootQuery
            //    .CustomField<SingleChemicalResultType, DefaultDetailInputType, 
            //        DefaultDetailInput>("Chemical", (args, dataContext) => new ChemicalResolver().GetOne(args, dataContext));
        }
    }
}
