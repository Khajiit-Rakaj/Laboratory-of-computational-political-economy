namespace LCPE.Data.BaseDataEntities;

public class NameValuePair
{
    public string Name { get; set; }

    public string Value { get; set; }

    public static NameValuePair Create(string name, string value)
    {
        return new NameValuePair
        {
            Name = name,
            Value = value
        };
    }
}