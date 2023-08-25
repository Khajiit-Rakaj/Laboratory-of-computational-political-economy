using Microsoft.Extensions.Configuration;

namespace LCPE.Extensions;

public static class ConfigurationExtensions
{
    public static List<T> GetListValues<T>(this IConfiguration configuration, string key)
    {
        var result = new List<T>();
        configuration.GetSection(key).Bind(result);

        return result;
    }
}