using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

// На перспективу надо будет добавить коллекцию для вложенных условий, по умолчанию использовать логическое И для полей
// для вложенных добавить опции ИЛИ, И НЕ (исключающее ИЛИ ?).
public interface IQuery
{
    public DataEntity DataEntity { get; set; }
    
    public IReturnFields ReturnFields { get; set; }
    
    public ISearchFields SearchFields { get; set; }

    public ISortingFields SortingFields { get; set; }

    public int PageSize { get; set; }

    public int FromPage { get; set; }
}