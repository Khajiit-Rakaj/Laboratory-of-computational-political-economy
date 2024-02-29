using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers;

public class CountryDynamicMapper: BaseDynamicMapper<Country>
{
    public CountryDynamicMapper()
    {
    }

    protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
    {
        Map(o => o.Id).Name(mappingConfiguration[nameof(Country.ShortName)]);
        Map(o => o.ShortName).Name(mappingConfiguration[nameof(Country.ShortName)]);
        Map(o => o.Name).Name(mappingConfiguration[nameof(Country.Name)]);
        Map(o => o.Capital).Name(mappingConfiguration[nameof(Country.Capital)]);
    }
}