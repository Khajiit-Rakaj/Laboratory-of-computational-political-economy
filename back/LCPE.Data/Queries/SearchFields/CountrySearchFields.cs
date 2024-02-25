using LCPE.Attributes;

namespace LCPE.Data.Queries.SearchFields;

public class CountrySearchFields : BaseSearchFields
{
    [FieldName("Name")]
    public IEnumerable<string> Names { get; set; }
    
    [FieldName("ShortName")]
    public IEnumerable<string> ShortNames { get; set; }
}