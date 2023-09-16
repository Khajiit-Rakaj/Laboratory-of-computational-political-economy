using LCPE.Attributes;

namespace LCPE.Data.Queries.SearchFields;

public class BaseSearchFields : ISearchFields
{
    [FieldName("Id")]
    public IEnumerable<string> Ids { get; set; }
}