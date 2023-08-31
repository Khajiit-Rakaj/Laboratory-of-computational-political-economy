using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;

namespace LCPE.DependencyInversionModules;

public class MiddlewareModule : Module
{
    private readonly IResolveMiddleware middleware;

    public MiddlewareModule(IResolveMiddleware middleware)
    {
        this.middleware = middleware;
    }

    protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistryBuilder,
        IComponentRegistration registration)
    {
        registration.PipelineBuilding += (sender, pipeline) => { pipeline.Use(middleware); };
    }
}
