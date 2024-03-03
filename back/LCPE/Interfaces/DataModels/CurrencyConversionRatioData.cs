using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels
{
    [CouchBaseRelation(DataConstants.CurrencyConversionRatio)]
    public class CurrencyConversionRatioData : DataEntityWithMetadata
    {
        [PresentableField(DataType.StringValue)]
        public string CurrencySoldShortName { get; set; }

        [PresentableField(DataType.StringValue)]
        public string CurrencyBoughtShortName { get; set; }

        [PresentableField(DataType.FloatPointValue)]
        public double Ratio { get; set; }

        [PresentableField(DataType.DateValue)]
        public DateTime Date {  get; set; }
    }
}
