using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers;

public class MetadataSourceDynamicMapper : BaseDynamicMapper<MetadataSource>
{
    protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
    {
        Map(o => o.Name).Name(mappingConfiguration[nameof(MetadataSource.Name)]);
        Map(o => o.Description).Name(mappingConfiguration[nameof(MetadataSource.Description)]);
        Map(o => o.ReliabilityLevel).Name(mappingConfiguration[nameof(MetadataSource.ReliabilityLevel)]);
    }
}