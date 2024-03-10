namespace LCPE.Data.Queries.SortingFields
{
    public class CurrencyConversionRatioSortingFields : ISortingFields
    {
        public SortingTypes CurrencySoldShortName { get; set; } = SortingTypes.None;

        public SortingTypes CurrencyBoughtShortName { get; set; } = SortingTypes.None;

        public SortingTypes Ratio { get; set; } = SortingTypes.None;

        public SortingTypes Date { get; set; } = SortingTypes.None;
    }
}
