using LCPE.Configurations;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Enums;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseCouchBaseRepository<TModel, TQuery> : BaseRepository<TModel>
    where TModel : DataEntity
    where TQuery : IQuery
{
    protected BaseCouchBaseRepository(ICouchBaseClientFactory<TModel> clientFactory, CouchBaseConfiguration options,
        ILog log) : base(clientFactory, options, log)
    {
    }

    public async Task<bool> RestoreIndexAsync()
    {
        return await Client.CreateCollectionAsync();
    }
    
    public virtual Task<IEnumerable<TModel>> SearchAsync(IQueryBuilder<TQuery> queryBuilder)
    {
        var returnFields = GetReturnFields(queryBuilder.Query.ReturnFields);
        var searchFields = GetSearchFields(queryBuilder.Query);
        var ordering = GetOrdering(queryBuilder.Query);
        var paging = GetPaging(queryBuilder.Query);

        var query = $"select {returnFields} from {Client.TablePlaceHolder}" +
                    $"{(searchFields.IsNotNullOrWhiteSpace() ? $" where {searchFields}" : string.Empty)}" +
                    $"{(ordering.IsNotNullOrWhiteSpace() ? $" order by {ordering}" : string.Empty)}" +
                    $"{paging}" as object;

        return Client.SearchAsync(query);
    }

    private string GetPaging(TQuery queryBuilderQuery)
    {
        return $" limit {queryBuilderQuery.PageSize} offset {queryBuilderQuery.PageSize * queryBuilderQuery.FromPage}";
    }

    protected string GetReturnFields(IReturnFields returnFields)
    {
        var returnFieldValues = returnFields != null
            ? string.Join(",",
                returnFields.GetType().GetProperties().Where(x => x.GetValue(returnFields).Equals(true))
                    .Select(x => x.Name))
            : string.Empty;

        return string.IsNullOrEmpty(returnFieldValues) ? "*" : returnFieldValues;
    }

    protected abstract string GetSearchFields(TQuery query);

    protected abstract string GetOrdering(TQuery query);

    protected void AddEqualsStatement(ref string searchStatement, string name, object value)
    {
        var formattedValue = FormatArgument(value);

        var statement = formattedValue.IsNotNullOrWhiteSpace() ? $"{name.LowerFirst()} = {formattedValue}" : string.Empty;

        searchStatement = searchStatement.IsNotNullOrWhiteSpace() ? $"{searchStatement} AND {statement}" : statement;
    }

    protected void AddArrayStatement(ref string searchStatement, string name, object value,
        LogicalOperations operationType = LogicalOperations.And)
    {
        var formattedValue = FormatArrayArgument(value);

        var statement = formattedValue.IsNotNullOrWhiteSpace() ? $"{name.LowerFirst()} in {formattedValue}" : string.Empty;

        if (statement.IsNotNullOrWhiteSpace())
        {
            searchStatement = searchStatement.IsNotNullOrWhiteSpace() && statement.IsNotNullOrWhiteSpace()
                ? $"{searchStatement} {operationType} {statement}"
                : statement;
        }
    }

    protected void AddSortingStatement(ref string searchStatement, string name, SortingTypes value)
    {
        var statement = value != SortingTypes.None ? $"{name.LowerFirst()} {value}" : string.Empty;

        if (statement.IsNotNullOrWhiteSpace())
        {
            searchStatement = searchStatement.IsNotNullOrWhiteSpace()
                ? $"{searchStatement},{statement}"
                : statement;
        }
    }

    private static string FormatArgument(object value)
    {
        switch (value)
        {
            case string:
            case DateTime:
                return $"\"{value}\"";
            case int:
            case double:
            case decimal:
                return value.ToString();
            default:
                return string.Empty;
        }
    }

    private static string FormatArrayArgument(object value)
    {
        return value switch
        {
            IEnumerable<string> stringValues => $"[\"{string.Join("\",\"", stringValues)}\"]",
            _ => string.Empty
        };
    }
}