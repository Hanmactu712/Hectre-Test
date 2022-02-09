using GraphQL.Types;
using Hectre.BackEnd.Models;

namespace Hectre.BackEnd.GraphQl
{
    public class BaseEntityType<T> : ObjectGraphType<T> where T: BaseEntity
    {
        public BaseEntityType()
        {
            Field(f => f.Id, type: typeof(StringGraphType)).Description("Id");
        }
    }
}
