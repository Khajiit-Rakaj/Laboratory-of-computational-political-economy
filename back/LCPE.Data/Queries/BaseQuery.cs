using LCPE.Constants;
using LCPE.Data.Interfaces;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Queries;

public class BaseQuery<T, TReturnFields, TSearchFields, TSortingFields> : IQuery
    where T: DataEntity
    where TReturnFields :IReturnFields 
    where TSearchFields : ISearchFields
    where TSortingFields : ISortingFields
{
    public T DataEntity { get; set; }
    
    DataEntity IQuery.DataEntity
    {
        get => DataEntity;
        set => DataEntity = (T) value;
    }
    
    public TReturnFields ReturnFields { get; set; }

    IReturnFields IQuery.ReturnFields
    {
        get => ReturnFields;
        set => ReturnFields = (TReturnFields) value;
    }

    public TSearchFields SearchFields { get; set; }

    ISearchFields IQuery.SearchFields
    {
        get => SearchFields;
        set => SearchFields = (TSearchFields) value;
    }

    public TSortingFields SortingFields { get; set; }

    ISortingFields IQuery.SortingFields
    {
        get => SortingFields;
        set => SortingFields = (TSortingFields) value;
    }

    public int PageSize { get; set; } = DataConstants.DefaultPageSize;

    public int FromPage { get; set; } = DataConstants.DefaultPage;
}