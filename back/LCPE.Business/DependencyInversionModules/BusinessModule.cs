using Autofac;
using LCPE.Business.Interfaces.Services;
using LCPE.Business.Services;

namespace LCPE.Business.DependencyInversionModules;

public class BusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TableService>().As<ITableService>();
    }

}