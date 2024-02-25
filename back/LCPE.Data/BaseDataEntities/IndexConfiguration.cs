namespace LCPE.Data.BaseDataEntities;

public class IndexConfiguration
{
    public string Index { get; set; }

    public string Scope { get; private init; }

    public static IndexConfiguration Create(string scope, string index = "")
    {
        return new IndexConfiguration
        {
            Scope = scope,
            Index = index
        };
    }
}