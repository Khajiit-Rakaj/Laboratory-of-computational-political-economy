// See https://aka.ms/new-console-template for more information

using Autofac;
using InfrastructureBuilder;
using InfrastructureBuilder.DependencyInversionModules;
using Microsoft.Extensions.Configuration;

args.ToList().ForEach(Console.WriteLine);
Console.WriteLine("Hello, World!");
var builder = new ContainerBuilder();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
DependencyRegistrationHelper.Register(builder, configuration);
var container = builder.Build();
var processor = container.Resolve<IConsoleProcessor>();

await processor.ContinuousProcessingAsync();