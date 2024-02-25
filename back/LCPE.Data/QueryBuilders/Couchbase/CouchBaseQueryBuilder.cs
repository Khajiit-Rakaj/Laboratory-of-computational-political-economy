using LCPE.Data.Interfaces;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Extensions;

namespace LCPE.Data.QueryBuilders.Couchbase;

public class CouchBaseQueryBuilder<TQuery> : IQueryBuilder<TQuery>
    where TQuery : IQuery
{
    public TQuery Query { get; }

    private CouchBaseQueryBuilder() : this(Activator.CreateInstance<TQuery>())
    {
    }
    
    private CouchBaseQueryBuilder(TQuery query)
    {
        Query = query;
    }
    
    public (Type, object) Build()
    {
        var fields = string.Empty;
        var table = Query.DataEntity.GetCouchBaseRelationCollection();
        var search = string.Empty;
        var sorting = string.Empty;
        var paging = string.Empty;

        var result = $"Select {fields} from {table}{search}{sorting}{paging}";

        return (result.GetType(), result);
    }

    public IQueryBuilder<TQuery> AddSearch<T>(Action<T>? action = null)
        where T : class, ISearchFields
    {
        Query.SearchFields = CreateOrReturn((T)Query.SearchFields);
        action?.Invoke((T)Query.SearchFields);

        return this;
    }

    public IQueryBuilder<TQuery> AddReturn<T>(Action<T>? action = null)
        where T : class, IReturnFields
    {
        Query.ReturnFields = CreateOrReturn((T)Query.ReturnFields);
        action?.Invoke((T)Query.ReturnFields);

        return this;
    }

    public IQueryBuilder<TQuery> AddSorting<T>(Action<T>? action = null)
        where T : class, ISortingFields
    {
        Query.SortingFields = CreateOrReturn((T)Query.SortingFields);
        action?.Invoke((T)Query.SortingFields);

        return this;
    }

    public IQueryBuilder<TQuery> FromPage(int page = 0)
    {
        Query.FromPage = page;

        return this;
    }

    public IQueryBuilder<TQuery> PageSize(int pageSize = 10)
    {
        Query.PageSize = pageSize;

        return this;
    }

    public static CouchBaseQueryBuilder<TQuery> Create()
    {
        return new CouchBaseQueryBuilder<TQuery>();
    }

    private static T CreateOrReturn<T>(T? existing) where T : class
    {
        return existing ?? Activator.CreateInstance<T>();
    }

    protected string BuildReturnFields()
    {
        var result = (string) null;
        
        

        return result ?? "*";
    }
}