using Autofac;
using InfrastructureBuilder.ActionInvokers;
using LCPE.Configurations;
using LCPE.Data.DependencyInversionModules;
using LCPE.Data.Interfaces.Repositories;
using LCPE.DependencyInversionModules;
using LCPE.Extensions;
using LCPE.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InfrastructureBuilder.DependencyInversionModules;

public static class DependencyRegistrationHelper
{
    public static void Register(ContainerBuilder builder, IConfiguration configuration)
    {
        var couchBaseConfiguration = configuration.GetSection(nameof(CouchBaseConfiguration)).Get<CouchBaseConfiguration>();
        
        LoggerConfiguration.Configure();
                
        builder.RegisterModule(new MiddlewareModule(new LoggerMiddleware()));

        builder.Register(x => couchBaseConfiguration).As<CouchBaseConfiguration>().SingleInstance();
        
        builder.RegisterModule<DataModule>();
        
        builder.RegisterType<ConsoleProcessor>().As<IConsoleProcessor>();
        builder.RegisterType<HelpActionInvoker>().As<IHelpActionInvoker>();

        builder.RegisterImplementations<IActionInvoker>();
        builder.RegisterImplementations<ICheckableRepository>();
        builder.RegisterImplementations<IDataEntity>();
    }
}