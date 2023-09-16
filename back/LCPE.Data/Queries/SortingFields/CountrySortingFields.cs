namespace LCPE.Data.Queries.SortingFields;

public class CountrySortingFields : ISortingFields
{
    public SortingTypes Name { get; set; } = SortingTypes.None;

    public SortingTypes ShortName { get; set; } = SortingTypes.None;
}