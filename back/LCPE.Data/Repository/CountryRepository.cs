using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Extensions;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository;

public class CountryRepository : BaseCouchBaseRepository<CountryData, CountryQuery>, ICountryRepository, ICheckableRepository
{
    public CountryRepository(ICouchBaseClientFactory<CountryData> clientFactory, CouchBaseConfiguration options, ILog log) :
        base(clientFactory, options, log)
    {
    }

    public Task<CountryData> GetAsync(string id)
    {
        return Client.GetAsync(id);
    }

    protected override string GetOrdering(CountryQuery query)
    {
        var result = string.Empty;
        var sortingFields = query.SortingFields;
        if (sortingFields == null)
        {
            return result;
        }

        AddSortingStatement(ref result, nameof(sortingFields.Name), sortingFields.Name);
        AddSortingStatement(ref result, nameof(sortingFields.ShortName), sortingFields.ShortName);
        
        return result;
    }

    protected override string GetSearchFields(CountryQuery query)
    {
        var result = string.Empty;
        var searchFields = query.SearchFields;
        if (searchFields == null)
        {
            return result;
        }

        AddArrayStatement(ref result, searchFields.GetFieldName(nameof(searchFields.Names)), searchFields.Names);
        AddArrayStatement(ref result, searchFields.GetFieldName(nameof(searchFields.ShortNames)), searchFields.ShortNames);
        AddArrayStatement(ref result, searchFields.GetFieldName(nameof(searchFields.Ids)), searchFields.Ids);

        return result;
    }
}