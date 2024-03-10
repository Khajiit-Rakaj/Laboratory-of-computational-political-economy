namespace LCPE.Data.Queries.SortingFields
{
    public class ResourceSortingFields : ISortingFields
    {
        public SortingTypes Year { get; set; } = SortingTypes.None;

        public SortingTypes ResourceName { get; set; } = SortingTypes.None;

        public SortingTypes ShortName { get; set; } = SortingTypes.None;

        public SortingTypes Organization { get; set; } = SortingTypes.None;

        public SortingTypes OriginCountry { get; set; } = SortingTypes.None;

        public SortingTypes Amount { get; set; } = SortingTypes.None;

        public SortingTypes Unit { get; set; } = SortingTypes.None;

        public SortingTypes Cost { get; set; } = SortingTypes.None;

        public SortingTypes Currency { get; set; } = SortingTypes.None;
    }
}
