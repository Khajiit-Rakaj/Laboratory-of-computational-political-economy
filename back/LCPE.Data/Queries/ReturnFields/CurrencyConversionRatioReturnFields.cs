namespace LCPE.Data.Queries.ReturnFields
{
    public class CurrencyConversionRatioReturnFields : BaseMetadataReturnFields
    {
        public bool CurrencySoldShortName { get; set; }

        public bool CurrencyBoughtShortName { get; set; }

        public bool Ratio { get; set; }

        public bool Date { get; set; }
    }
}
