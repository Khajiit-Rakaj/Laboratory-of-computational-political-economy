using LCPE.Data.DataBuilders.Converters;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers
{
    public class OrganisationDynamicMapper : BaseDynamicMapper<OrganisationData>
    {
        protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
        {
            Map(o => o.Id).Name(mappingConfiguration[nameof(OrganisationData.Name)]);
            Map(o => o.Name).Name(mappingConfiguration[nameof(OrganisationData.Name)]);
            Map(o => o.ShortName).Name(mappingConfiguration[nameof(OrganisationData.ShortName)]);
            Map(o => o.Type).TypeConverter<HistoricalDataConverter>().Name(mappingConfiguration[nameof(OrganisationData.Type)]);
            Map(o => o.NameHistory).TypeConverter<HistoricalDataConverter>().Name(mappingConfiguration[nameof(OrganisationData.NameHistory)]);
            Map(o => o.Abbreviation).Name(mappingConfiguration[nameof(OrganisationData.Abbreviation)]);
            Map(o => o.Country).Name(mappingConfiguration[nameof(OrganisationData.Country)]);
            Map(o => o.CountryHistory).TypeConverter<HistoricalDataConverter>().Name(mappingConfiguration[nameof(OrganisationData.CountryHistory)]);
            Map(o => o.CreationDate).Name(mappingConfiguration[nameof(OrganisationData.CreationDate)]);
            Map(o => o.EndDate).Name(mappingConfiguration[nameof(OrganisationData.EndDate)]);
            Map(o => o.Maternal).Name(mappingConfiguration[nameof(OrganisationData.Maternal)]);
            Map(o => o.MaternalSharing).TypeConverter<MaternalSharingConverter>().Name(mappingConfiguration[nameof(OrganisationData.MaternalSharing)]);
        }
    }
}
