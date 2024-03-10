namespace LCPE.Data.Queries.SortingFields
{
    public class ProductSortingFields : ISortingFields
    {
        public SortingTypes Year { get; set; } = SortingTypes.None;

        public SortingTypes ProductName { get; set; } = SortingTypes.None;

        public SortingTypes ShortName { get; set; } = SortingTypes.None;

        public SortingTypes Organization { get; set; } = SortingTypes.None;

        public SortingTypes OriginCountry { get; set; } = SortingTypes.None;

        public SortingTypes Amount { get; set; } = SortingTypes.None;

        public SortingTypes Unit { get; set; } = SortingTypes.None;

        public SortingTypes Cost { get; set; } = SortingTypes.None;

        public SortingTypes Currency { get; set; } = SortingTypes.None;
    }
}
