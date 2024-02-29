namespace LCPE.Extensions;

public static class DictionaryExtensions
{
    public static void ForEach<T, V>(this IDictionary<T, V> dictionary, Action<KeyValuePair<T, V>> action)
    {
        foreach (var keyValuePair in dictionary)
        {
            action(keyValuePair);
        }
    }
}