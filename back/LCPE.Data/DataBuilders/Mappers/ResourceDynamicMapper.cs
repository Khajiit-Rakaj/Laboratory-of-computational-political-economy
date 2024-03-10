using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers
{
    public class ResourceDynamicMapper : BaseDynamicMapper<ResourceData>
    {
        protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
        {
            Map(r => r.Id).Name(mappingConfiguration[nameof(ResourceData.ResourceName)]);
            Map(r => r.ResourceName).Name(mappingConfiguration[nameof(ResourceData.ResourceName)]);
            Map(r => r.ShortName).Name(mappingConfiguration[nameof(ResourceData.ShortName)]);
            Map(r => r.Year).Name(mappingConfiguration[nameof(ResourceData.Year)]);
            Map(r => r.Organization).Name(mappingConfiguration[nameof(ResourceData.Organization)]);
            Map(r => r.OriginCountry).Name(mappingConfiguration[nameof(ResourceData.OriginCountry)]);
            Map(r => r.Amount).Name(mappingConfiguration[nameof(ResourceData.Amount)]);
            Map(r => r.Unit).Name(mappingConfiguration[nameof(ResourceData.Unit)]);
            Map(r => r.Cost).Name(mappingConfiguration[nameof(ResourceData.Cost)]);
            Map(r => r.Currency).Name(mappingConfiguration[nameof(ResourceData.Currency)]);
        }
    }
}
