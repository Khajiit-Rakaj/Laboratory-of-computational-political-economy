using System.Reflection;
using LCPE.Attributes;
using LCPE.Data.Queries.SearchFields;

namespace LCPE.Data.Extensions;

public static class DataFieldsExtensions
{
    public static string GetFieldName(this ISearchFields dataEntity, string propertyName)
    {
        return dataEntity.GetType().GetProperty(propertyName)?.GetCustomAttribute<FieldNameAttribute>()?.FieldName ?? string.Empty;
    }
}