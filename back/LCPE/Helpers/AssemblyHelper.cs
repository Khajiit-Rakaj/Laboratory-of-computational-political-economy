using System.Reflection;

namespace LCPE.Helpers;

public static class AssemblyHelper
{
    private static readonly string AssemblyName = "LCPE";

    public static IEnumerable<Assembly> GetExecutingAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic &&
                        x.FullName.StartsWith(AssemblyName, StringComparison.InvariantCultureIgnoreCase));
    }
}