using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;

namespace LCPE.Data.Interfaces;

public interface IQueryBuilder<TQuery> where TQuery : IQuery
{
    public TQuery Query { get; }
    IQueryBuilder<TQuery> AddSearch<T>(Action<T>? action = null) where T : class, ISearchFields;
    IQueryBuilder<TQuery> AddReturn<T>(Action<T>? action = null) where T : class, IReturnFields;
    IQueryBuilder<TQuery> AddSorting<T>(Action<T>? action = null) where T : class, ISortingFields;
    IQueryBuilder<TQuery> FromPage(int page = 0);
    IQueryBuilder<TQuery> PageSize(int pageSize = 10);
}