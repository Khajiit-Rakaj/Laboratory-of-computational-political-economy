namespace LCPE.Attributes;

public class CouchBaseRelationAttribute : Attribute
{
    public readonly string CollectionName;

    public CouchBaseRelationAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}