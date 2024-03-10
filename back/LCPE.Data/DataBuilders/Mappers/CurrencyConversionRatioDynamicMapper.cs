using CsvHelper.TypeConversion;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers
{
    public class CurrencyConversionRatioDynamicMapper : BaseDynamicMapper<CurrencyConversionRatioData>
    {
        protected override void CreateMapping(IDictionary<string, string> mappingConfiguration)
        {
            Map(c => c.CurrencyBoughtShortName).Name(mappingConfiguration[nameof(CurrencyConversionRatioData.CurrencyBoughtShortName)]);
            Map(c => c.CurrencySoldShortName).Name(mappingConfiguration[nameof(CurrencyConversionRatioData.CurrencySoldShortName)]);
            Map(c => c.Ratio).Name(mappingConfiguration[nameof(CurrencyConversionRatioData.Ratio)]);
            Map(c => c.Date).TypeConverter<DateTimeConverter>().TypeConverterOption.Format("dd.MM.yyyy")
                .Name(mappingConfiguration[nameof(CurrencyConversionRatioData.Date)]);
        }
    }
}
