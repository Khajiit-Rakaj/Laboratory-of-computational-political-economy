using LCPE.Data.BaseDataEntities;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Helpers;

public static class DataPreparerHelper
{
    private static string EnumerableToString(IEnumerable<object> enumerable)
    {
        if (enumerable == null)
        {
            return string.Empty;
        }

        string res = "";
        foreach (object item in enumerable)
        {
            res += item.ToString() + ";";
        }

        return res;
    }

    public static IEnumerable<IEnumerable<NameValuePair>> PrepareData<T>(
        ICollection<ColumnDataModel> columns,
        IEnumerable<T> data)
    {
        var propertiesInfo = typeof(T).GetProperties().Where(x => columns.Any(c => c.Name == x.Name)).ToList();
        var dataRows = data.Select(x =>
            propertiesInfo.Select(c => {
                var val = c.GetValue(x);
                if (val == null)
                {
                    return NameValuePair.Create(c.Name, "");
                }

                bool isEnumerable = Array.Exists(val.GetType().GetInterfaces(),
                    i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                return NameValuePair.Create(c.Name, isEnumerable && val.GetType() != typeof(string) ?
                    EnumerableToString((IEnumerable<object>)val) : val.ToString());
                }));

        return dataRows;
    }
}