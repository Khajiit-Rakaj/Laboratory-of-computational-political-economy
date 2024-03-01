using LCPE.Data.Interfaces;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.Services;

public interface ICountryService : IDataEntityService<CountryQuery>
{
    Task<Country> GetAsync(string id);

    Task<IEnumerable<Country>> SearchAsync(IQueryBuilder<CountryQuery> queryBuilder);

}