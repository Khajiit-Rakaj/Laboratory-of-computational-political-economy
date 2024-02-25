namespace LCPE.Extensions;

public static class List
{
    public static List<T> Create<T>(params T[] items) => items.ToList();

    public static IEnumerable<T> YieldAsEnumerable<T>(this T item)
    {
        yield return item;
    }
}