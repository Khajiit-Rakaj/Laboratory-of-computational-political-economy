using Autofac;
using LCPE.Data.CouchBase;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Repository;

namespace LCPE.Data.DependencyInversionModules;

public class DataModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        
        
        builder.RegisterType<CouchBaseClientFactory>().As<ICouchBaseClientFactory>().SingleInstance();

        builder.RegisterType<TableRepository>().As<ITablesRepository>();
    }
}