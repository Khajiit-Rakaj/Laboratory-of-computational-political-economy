using LCPE.Interfaces;

namespace LCPE.Data.Interfaces.CsvDataMappers;

public interface IDynamicDataMapper
{
    Type GetEntityType { get; }

    void Initialize(IDictionary<string, string> mappingConfiguration);
}