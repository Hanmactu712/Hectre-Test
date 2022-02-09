using GraphQL.Types;
using System;
using System.Collections.Generic;

namespace Hectre.BackEnd.GraphQl
{
    public class DefaultListInputType<T> : InputObjectGraphType<T> where T : DefaultListInput
    {
        public DefaultListInputType()
        {
            Name = $"{typeof(T).Name}Type";
            Field(e => e.Ids, type: typeof(ListGraphType<StringGraphType>), nullable: true);
            Field(e => e.Keyword, type: typeof(StringGraphType), nullable: true);
            Field(e => e.Start, type: typeof(IntGraphType), nullable: true);
            Field(e => e.Limit, type: typeof(IntGraphType), nullable: true);
            Field(e => e.Sort, type: typeof(ListGraphType<StringGraphType>), nullable: true);
            Field(e => e.Where, type: typeof(StringGraphType), nullable: true);
        }
    }

    public class DefaultListInput
    {
        public List<string> Ids { get; set; }
        public string Keyword { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public List<string> Sort { get; set; }
        public string Where { get; set; }
    }
}
