﻿using Autofac;
using LCPE.Business.Interfaces.Services;
using LCPE.Business.Services;

namespace LCPE.Business.DependencyInversionModules;

public class BusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TableService>().As<ITableService>();
        builder.RegisterType<CountryService>().As<ICountryService>();
        builder.RegisterType<CorporationFinancesService>().As<ICorporationFinancesService>();
        builder.RegisterType<OrganisationService>().As<IOrganisationService>();
        builder.RegisterType<MetadataSourceService>().As<IMetadataSourceService>();
        builder.RegisterType<CsvDataUploaderService>().As<ICsvDataUploaderService>();
    }

}