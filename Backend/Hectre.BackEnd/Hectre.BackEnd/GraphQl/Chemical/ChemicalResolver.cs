using GraphQL;
using Hectre.BackEnd.Common;
using Hectre.BackEnd.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hectre.BackEnd.GraphQl.Chemical
{
    /// <summary>
    /// Contains all field resolvers for Chemical Schema
    /// </summary>
    public class ChemicalResolver
    {
        public DefaultListResult<Models.Chemical> GetList(DefaultListInput args, DataContext dataContext)
        {
            try
            {
                if (args == null)
                    return new DefaultListResult<Models.Chemical>()
                    {
                        Data = null,
                        Total = 0,
                        Code = Constants.ErrorCode.ERR_100,
                        Message = Constants.ErrorMessage.InvalidInput
                    };

                var queryCondition = GetQueryConditionFromArguments(args);
                var sortCondition = GetSortConditionFromArguments(args);

                var countSpec = new EntitySpecs<Models.Chemical>(queryCondition);
                var totalCount = dataContext.ChemicalRepository.Count(countSpec);
                
                var querySpec = new EntitySpecs<Models.Chemical>(queryCondition, sortCondition.SortField, sortCondition.SortDirection, args.Start, args.Limit);
                var data = dataContext.ChemicalRepository.List(querySpec);

                return new DefaultListResult<Models.Chemical>()
                {
                    Data = data.ToList(),
                    Total = totalCount
                };
            }
            catch (Exception ex)
            {
                return new DefaultListResult<Models.Chemical>()
                {
                    Data = null,
                    Total = 0,
                    Code = Constants.ErrorCode.ERR_500,
                    Message = ex.Message
                };
            }
        }

        private Expression<Func<Models.Chemical, bool>> GetQueryConditionFromArguments(DefaultListInput args)
        {
            Expression<Func<Models.Chemical, bool>> condition = e => true;

            if (args == null) return condition;

            if (args.Ids != null && args.Ids.Count > 0)
            {
                condition = condition.And(e => args.Ids.Contains(e.Id));
            }

            return condition;
        }

        private SortCondition<Models.Chemical> GetSortConditionFromArguments(DefaultListInput args)
        {
            Expression<Func<Models.Chemical, object>> sortExpression = e => e.Name;
            var sortDirection = "asc";

            if (args == null)
                return new SortCondition<Models.Chemical>()
                {
                    SortDirection = sortDirection,
                    SortField = sortExpression
                };

            if (args.Sort != null && args.Sort.Count > 0)
            {
                var sortStr = args.Sort.FirstOrDefault()?.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (sortStr != null && sortStr.Count > 1)
                {
                    sortExpression = ExpressionExtension.GetSortExpressionByName<Models.Chemical>(sortStr[0].ToPascalCase());
                    sortDirection = sortStr[1];
                }
            }

            return new SortCondition<Models.Chemical>()
            {
                SortDirection = sortDirection,
                SortField = sortExpression
            };
        }

        public DefaultSingleResult<Models.Chemical> GetOne(DefaultDetailInput args, DataContext dataContext)
        {
            try
            {
                var id = args.Id;

                var result = dataContext.ChemicalRepository.GetById(id);

                return new DefaultSingleResult<Models.Chemical>()
                {
                    Data = result,
                    Total = 1,
                };
            }
            catch (Exception ex)
            {
                return new DefaultSingleResult<Models.Chemical>()
                {
                    Data = null,
                    Total = 1,
                    Code = Constants.ErrorCode.ERR_500,
                    Message = ex.Message
                };
            }
        }

        public DefaultSingleResult<Models.Chemical> AddChemical(Models.Chemical args, DataContext dataContext)
        {
            var resultData = new DefaultSingleResult<Models.Chemical>();

            try
            {
                var chemical = args;

                if (chemical == null)
                {
                    resultData.Code = Constants.ErrorCode.ERR_100;
                    resultData.Message = Constants.ErrorMessage.InvalidInput;

                    return resultData;
                }

                if (string.IsNullOrEmpty(chemical.Name) || string.IsNullOrEmpty(chemical.ActiveIngredient) ||
                    string.IsNullOrEmpty(chemical.ChemicalType) ||
                    string.IsNullOrEmpty(chemical.PreHarvestIntervalInDays))
                {
                    resultData.Code = Constants.ErrorCode.ERR_200;
                    resultData.Message = Constants.ErrorMessage.MissingMandatoryFields;
                    return resultData;
                }

                chemical.Id = Guid.NewGuid().ToString();
                chemical.CreationDate = DateTime.Now;
                chemical.ModificationDate = DateTime.Now;
                chemical.DeletionDate = null;

                var result = dataContext.ChemicalRepository.Add(chemical);

                resultData.Data = result;
                resultData.Total = 1;
                return resultData;
            }
            catch (Exception ex)
            {
                resultData.Code = Constants.ErrorCode.ERR_500;
                resultData.Message = ex.Message;
                return resultData;
            }
        }

        //public object UpdateChemical(Models.Chemical args, DataContext dataContext)
        //{
        //    try
        //    {
        //        var Chemical = args;

        //        var existChemical = dataContext.ChemicalRepository.GetById(Chemical.Id);
        //        Chemical.MapTo(existChemical);
        //        Chemical.ModifiedDate = DateTime.Now;
        //        Chemical.ModifiedBy = Guid.NewGuid();

        //        dataContext.ChemicalRepository.Update(existChemical);

        //        return new DefaultSingleResult<Models.Chemical>()
        //        {
        //            Data = existChemical,
        //            Total = 1,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new DefaultSingleResult<Models.Chemical>()
        //        {
        //            Data = null,
        //            Total = 1,
        //            Code = Constants.ErrorCode.ERR_500,
        //            Message = ex.Message
        //        };
        //    }
        //}

        //public object DeleteChemical(Models.Chemical args, DataContext dataContext)
        //{
        //    try
        //    {
        //        var existChemical = dataContext.ChemicalRepository.GetById(args.Id);
        //        dataContext.ChemicalRepository.Delete(existChemical);

        //        return new DefaultSingleResult<Models.Chemical>()
        //        {
        //            Data = existChemical,
        //            Total = 1,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new DefaultSingleResult<Models.Chemical>()
        //        {
        //            Data = null,
        //            Total = 1,
        //            Code = Constants.ErrorCode.ERR_500,
        //            Message = ex.Message
        //        };
        //    }
        //}
    }
}
