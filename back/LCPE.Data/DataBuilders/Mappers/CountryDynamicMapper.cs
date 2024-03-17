using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers;

public class CountryDynamicMapper : BaseDynamicMapper<CountryData>
{
    protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
    {
        Map(o => o.Id).Name(mappingConfiguration[nameof(CountryData.ShortName)]);
        Map(o => o.ShortName).Name(mappingConfiguration[nameof(CountryData.ShortName)]);
        Map(o => o.Name).Name(mappingConfiguration[nameof(CountryData.Name)]);
        Map(o => o.Capital).Name(mappingConfiguration[nameof(CountryData.Capital)]);
    }
}