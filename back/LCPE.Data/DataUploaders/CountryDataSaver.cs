using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders;

public class CountryDataSaver : BaseEntityDataSaver<Country, CountryQuery>
{
    public CountryDataSaver(ICountryRepository repository) : base(repository)
    {
    }
}