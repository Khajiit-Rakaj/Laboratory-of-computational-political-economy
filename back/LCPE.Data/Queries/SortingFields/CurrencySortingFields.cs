namespace LCPE.Data.Queries.SortingFields
{
    public class CurrencySortingFields : ISortingFields
    {
        public SortingTypes Name { get; set; } = SortingTypes.None;

        public SortingTypes Sign { get; set; } = SortingTypes.None;
    }
}
