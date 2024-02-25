using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using LCPE.Helpers;

namespace LCPE.Extensions;

public static class RegistrationBuilderExtensions
{
    public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
        RegisterImplementations<T>(this ContainerBuilder builder)
    {
        return builder.RegisterAssemblyTypes(AssemblyHelper.GetExecutingAssemblies().ToArray())
            .Where(t => typeof(T).IsAssignableFrom(t))
            .AsImplementedInterfaces();
    }

}