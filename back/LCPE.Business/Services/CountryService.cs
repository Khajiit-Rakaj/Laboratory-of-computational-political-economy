using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        this.countryRepository = countryRepository;
    }
    
    public async Task<Country> GetAsync(string id)
    {
        return await countryRepository.GetAsync(id);
    }

    public async Task<IEnumerable<Country>> SearchAsync()
    {
        return await countryRepository.SearchAsync();
    }
}