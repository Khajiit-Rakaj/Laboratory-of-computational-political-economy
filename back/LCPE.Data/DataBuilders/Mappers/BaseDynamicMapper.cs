using CsvHelper.Configuration;
using LCPE.Data.Interfaces.CsvDataMappers;
using LCPE.Interfaces;

namespace LCPE.Data.DataBuilders.Mappers;

public abstract class BaseDynamicMapper<TModel> : ClassMap<TModel>, IDynamicDataMapper where TModel : IDataEntity
{
    public Type GetEntityType => typeof(TModel);

    public void Initialize(IDictionary<string, string> mappingConfiguration)
    {
        CreateMapping(mappingConfiguration);
    }

    protected abstract void CreateMapping(IDictionary<string, string> mappingConfiguration);
}