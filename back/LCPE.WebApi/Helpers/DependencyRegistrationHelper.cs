using Autofac;
using LCPE.Business.DependencyInversionModules;
using LCPE.Data.DependencyInversionModules;

namespace WebApi.Helpers;

public class DependencyRegistrationHelper
{
    public static void Register(ContainerBuilder builder, IConfiguration configuration)
    {
        builder.RegisterModule<BusinessModule>();
        builder.RegisterModule<DataModule>();
    }
}