using Autofac;
using Autofac.Extensions.DependencyInjection;
using LCPE;
using LCPE.Configurations;
using LCPE.DependencyInversionModules;
using LCPE.Extensions;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CouchBaseConfiguration>(builder.Configuration.GetSection(nameof(ConfigKeys.AllowedCors)));

builder.Host.UseWindowsService();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((c, b) =>
    {
        b.RegisterModule(new CommonModule(builder.Configuration));
        DependencyRegistrationHelper.Register(b, builder.Configuration);
    });

LoggerConfiguration.Configure();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
var allowedOrigins = builder.Configuration.GetListValues<string>(ConfigKeys.AllowedCors).ToArray();
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(allowedOrigins));

app.MapControllerRoute("default", "api/{controller}/{id?}");

await app.RunAsync();