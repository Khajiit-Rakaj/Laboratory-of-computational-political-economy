using LCPE.Business.Interfaces.Services;
using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Helpers;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class CountryService : BaseDataEntityService, ICountryService
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

    public async Task<IEnumerable<Country>> SearchAsync(IQueryBuilder<CountryQuery> queryBuilder)
    {
        return await countryRepository.SearchAsync(queryBuilder);
    }

    public async Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<CountryQuery> queryBuilder)
    {
        var data = (await countryRepository.SearchAsync(queryBuilder)).ToList();
        var fields = GetFields<Country, CountryReturnFields>(queryBuilder.Query.ReturnFields).ToList();
        var returnData = DataPreparerHelper.PrepareData(fields, data);

        return WorkTableViewModel.Create(
            nameof(Country),
            List.Create(typeof(Country).GetCouchBaseRelationCollection()),
            fields,
            returnData);
    }
}