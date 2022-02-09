using GraphQL.Types;

namespace Hectre.BackEnd.GraphQl.Chemical
{
    public sealed class CreateChemicalInputType : InputObjectGraphType<Models.Chemical>
    {
        public CreateChemicalInputType()
        {
            Name = "createChemicalInput";
            Field(f => f.ChemicalType, type: typeof(StringGraphType)).Description("Chemical Type");
            Field(f => f.ActiveIngredient, type: typeof(StringGraphType)).Description("Active Ingredient");
            Field(f => f.PreHarvestIntervalInDays, type: typeof(StringGraphType)).Description("Pre Harvest Interval In Days");
            Field(f => f.Name, type: typeof(StringGraphType)).Description("Name");
        }
    }
}
