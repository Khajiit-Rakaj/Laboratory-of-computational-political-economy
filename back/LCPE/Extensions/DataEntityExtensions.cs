using System.Reflection;
using LCPE.Attributes;
using LCPE.Interfaces;
using LCPE.Interfaces.DataModels;

namespace LCPE.Extensions;

public static class DataEntityExtensions
{
    public static string GetCouchBaseRelationCollection(this IDataEntity dataEntity)
    {
        return dataEntity.GetType().GetCustomAttribute<CouchBaseRelationAttribute>()?.CollectionName ?? string.Empty;
    }

    public static string GetCouchBaseRelationCollection(this Type dataEntity)
    {
        return dataEntity.GetCustomAttribute<CouchBaseRelationAttribute>()?.CollectionName ?? string.Empty;
    }

    public static ICollection<ColumnDataModel> GetColumnDataModels(this IDataEntity dataEntity,
        bool onlyPresentable = true)
    {
        var properties = dataEntity.GetType().GetProperties().Where(x =>
            x.GetCustomAttribute<PresentableFieldAttribute>() != null ||
            !onlyPresentable && x.GetCustomAttribute<ServiceFieldAttribute>() != null);

        var result = properties.Select(x => ColumnDataModel.Create(x.Name,
            x.GetCustomAttribute<PresentableFieldAttribute>()?.DataType ??
            x.GetCustomAttribute<ServiceFieldAttribute>()!.DataType,
            x.GetCustomAttribute<PresentableFieldAttribute>() != null,
            string.Empty)).ToList();

        return result;
    }
    
    public static bool IsServiceTable(this IDataEntity dataEntity)
    {
        return dataEntity.GetType().GetCustomAttribute<ServiceTableAttribute>() != null;
    }
}