namespace LCPE.Extensions;

public static class StringExtensions
{
    public static bool IsNotNullOrWhiteSpace(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static string LowerFirst(this string value)
    {
        return char.ToLower(value[0]) + value[1..];
    }
}