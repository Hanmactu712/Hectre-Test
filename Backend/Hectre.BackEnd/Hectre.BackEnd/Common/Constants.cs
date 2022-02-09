
namespace Hectre.BackEnd.Common
{
    public class Constants
    {
        public const string DbSchema = "hectre_db";
        public const string ASC = "asc";
        public const string DESC = "desc";


        public class ErrorCode
        {
            public const string ERR_500 = "500";
            public const string ERR_200 = "200";
            public const string ERR_100 = "100";
        }

        public class ErrorMessage
        {
            public const string MissingMandatoryFields = "Missing mandatory fields";
            public const string InvalidInput = "Invalid input";
        }

        public class GraphQuery
        {
            public class ChemicalQuery
            {
                public const string GetList = "chemicals";
                public const string GetOne = "chemical";
            }

            public class ChemicalMutation
            {
                public const string CreateChemical = "createChemical";
                public const string UpdateChemical = "updateChemical";
            }
        }
    }
}
