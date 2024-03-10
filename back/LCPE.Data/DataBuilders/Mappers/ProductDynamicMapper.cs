using LCPE.Data.DataBuilders.Converters;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers
{
    public class ProductDynamicMapper : BaseDynamicMapper<ProductData>
    {
        protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
        {
            Map(p => p.Id).Name(mappingConfiguration[nameof(ProductData.ShortName)]);
            Map(p => p.ShortName).TypeConverter<StringArrayConverter>().Name(mappingConfiguration[nameof(ProductData.ShortName)]);
            Map(p => p.Year).Name(mappingConfiguration[nameof(ProductData.Year)]);
            Map(p => p.ProductName).Name(mappingConfiguration[nameof(ProductData.ProductName)]);
            Map(p => p.Organization).Name(mappingConfiguration[nameof(ProductData.Organization)]);
            Map(p => p.OriginCountry).Name(mappingConfiguration[nameof(ProductData.OriginCountry)]);
            Map(p => p.Amount).Name(mappingConfiguration[nameof(ProductData.Amount)]);
            Map(p => p.Unit).Name(mappingConfiguration[nameof(ProductData.Unit)]);
            Map(p => p.Currency).Name(mappingConfiguration[nameof(ProductData.Currency)]);
            Map(p => p.Cost).Name(mappingConfiguration[nameof(ProductData.Cost)]);
        }
    }
}
