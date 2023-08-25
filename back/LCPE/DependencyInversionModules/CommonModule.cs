using Autofac;
using LCPE.Configurations;
using Microsoft.Extensions.Configuration;

namespace LCPE.DependencyInversionModules;

public class CommonModule : Module
{
    private readonly IConfiguration configuration;
    private readonly CouchBaseConfiguration couchBaseConfiguration;

    public CommonModule(IConfiguration configuration)
    {
        this.configuration = configuration;
        couchBaseConfiguration = configuration.GetSection(nameof(CouchBaseConfiguration)).Get<CouchBaseConfiguration>();
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(x => configuration).As<IConfiguration>().SingleInstance();
        builder.Register(x => couchBaseConfiguration).As<CouchBaseConfiguration>().SingleInstance();
    }
}