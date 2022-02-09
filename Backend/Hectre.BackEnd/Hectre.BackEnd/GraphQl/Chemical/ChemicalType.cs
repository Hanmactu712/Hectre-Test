using GraphQL.Types;

namespace Hectre.BackEnd.GraphQl.Chemical
{
    public sealed class ChemicalType : BaseEntityType<Models.Chemical>
    {
        public ChemicalType()
        {
            Field(f => f.ChemicalType, type: typeof(StringGraphType)).Description("Chemical Type");
            Field(f => f.ActiveIngredient, type: typeof(StringGraphType)).Description("Active Ingredient");
            Field(f => f.PreHarvestIntervalInDays, type: typeof(StringGraphType)).Description("Pre Harvest Interval In Days");
            Field(f => f.Name, type: typeof(StringGraphType)).Description("Name");
            Field(f => f.CreationDate, type: typeof(DateTimeGraphType)).Description("Creation Date");
            Field(f => f.ModificationDate, type: typeof(DateTimeGraphType)).Description("Modification Date");
            Field(f => f.DeletionDate, type: typeof(DateTimeGraphType)).Description("Deletion Date");
        }
    }

    public class ListChemicalResultType : DefaultListResultType<Models.Chemical, ChemicalType>
    {
        public ListChemicalResultType() : base()
        {
            Name = "ListChemicalResultType";
        }
    }


    public class SingleChemicalResultType : DefaultSingleResultType<Models.Chemical, ChemicalType>
    {
        public SingleChemicalResultType() : base()
        {
            Name = "SingleChemicalResultType";
        }
    }
}
