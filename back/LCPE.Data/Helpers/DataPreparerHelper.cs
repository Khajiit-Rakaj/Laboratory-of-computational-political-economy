using LCPE.Data.BaseDataEntities;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Helpers;

public static class DataPreparerHelper
{
    public static IEnumerable<IEnumerable<NameValuePair>> PrepareData<T>(
        ICollection<ColumnDataModel> columns,
        IEnumerable<T> data)
    {
        var propertiesInfo = typeof(T).GetProperties().Where(x => columns.Any(c => c.Name == x.Name)).ToList();
        var dataRows = data.Select(x =>
            propertiesInfo.Select(c => NameValuePair.Create(c.Name, c.GetValue(x)?.ToString())));

        return dataRows;
    }
}