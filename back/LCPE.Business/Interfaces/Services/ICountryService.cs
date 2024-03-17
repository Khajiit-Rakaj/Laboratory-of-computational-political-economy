﻿using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.Services;

public interface ICountryService : IDataEntityService<CountryData, CountryQuery>
{
}