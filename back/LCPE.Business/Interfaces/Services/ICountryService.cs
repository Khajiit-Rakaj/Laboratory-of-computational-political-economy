using LCPE.Data.Interfaces;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.Services;

public interface ICountryService : IDataEntityService<CountryQuery>
{
    Task<CountryData> GetAsync(string id);

    Task<IEnumerable<CountryData>> SearchAsync(IQueryBuilder<CountryQuery> queryBuilder);

}