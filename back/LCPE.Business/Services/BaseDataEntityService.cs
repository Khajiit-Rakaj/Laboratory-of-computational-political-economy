using LCPE.Data.Queries.ReturnFields;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class BaseDataEntityService
{
    protected IEnumerable<ColumnDataModel> GetFields<T, TReturnFields>(TReturnFields queryReturnFields) where TReturnFields : class, IReturnFields
        where T : DataEntity
    {
        var returnFieldNames = GetReturnFieldNames(queryReturnFields);
        var returnFields = typeof(T).GetColumnDataModels().Where(x => returnFieldNames.Contains(x.Name));

        return returnFields;
    }

    private IEnumerable<string> GetReturnFieldNames<TReturnFields>(TReturnFields queryReturnFields) where TReturnFields : class, IReturnFields
    {
        var returnFields = (queryReturnFields?.GetType() ?? typeof(TReturnFields)).GetProperties().ToList();

        var setToReturnFields = returnFields.Where(x => queryReturnFields == null || (bool?)x.GetValue(queryReturnFields) == true).ToList();

        return (setToReturnFields.Any() ? setToReturnFields : returnFields).Select(x => x.Name);
    }
}