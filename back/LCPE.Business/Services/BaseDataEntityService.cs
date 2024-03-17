using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Helpers;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class BaseDataEntityService<TModel, TQuery, TReturnFields, TSearchFields, TSortingFields>
    where TModel: DataEntity
    where TQuery: BaseQuery <TModel, TReturnFields, TSearchFields, TSortingFields>
    where TReturnFields: class, IReturnFields
    where TSearchFields: class, ISearchFields
    where TSortingFields: class, ISortingFields
{
    private readonly IBaseRepository<TModel, TQuery> repository;

    protected BaseDataEntityService(IBaseRepository<TModel, TQuery> repository)
    {
        this.repository = repository;
    }
    
    public async Task<TModel?> GetAsync(string id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<IEnumerable<TModel>> SearchAsync(IQueryBuilder<TQuery> queryBuilder)
    {
        return await repository.SearchAsync(queryBuilder);
    }

    public async Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<TQuery> queryBuilder)
    {
        var data = (await repository.SearchAsync(queryBuilder)).ToList();
        var fields = GetFields(queryBuilder.Query.ReturnFields).ToList();
        var returnData = DataPreparerHelper.PrepareData(fields, data);

        return WorkTableViewModel.Create(
            typeof(TModel).Name,
            List.Create(typeof(TModel).GetCouchBaseRelationCollection()),
            fields,
            returnData);
    }

    private IEnumerable<ColumnDataModel> GetFields(TReturnFields queryReturnFields)
    {
        var returnFieldNames = GetReturnFieldNames(queryReturnFields);
        var returnFields = typeof(TModel).GetColumnDataModels().Where(x => returnFieldNames.Contains(x.Name));

        return returnFields;
    }

    private static IEnumerable<string> GetReturnFieldNames(TReturnFields queryReturnFields)
    {
        var returnFields = (queryReturnFields?.GetType() ?? typeof(TReturnFields)).GetProperties().ToList();

        var setToReturnFields = returnFields.Where(x => queryReturnFields == null || (bool?)x.GetValue(queryReturnFields) == true).ToList();

        return (setToReturnFields.Any() ? setToReturnFields : returnFields).Select(x => x.Name);
    }
}