using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(ICouchBaseClientFactory<Country> clientFactory, CouchBaseConfiguration options, ILog log) :
        base(clientFactory, options, log)
    {
    }

    public Task<Country> GetAsync(string id)
    {
        return client.GetAsync(id);
    }

    public Task<IEnumerable<Country>> SearchAsync()
    {
        return client.SearchAsync();
    }
}