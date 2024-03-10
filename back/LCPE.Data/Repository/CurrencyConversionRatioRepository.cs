using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository
{
    public class CurrencyConversionRatioRepository : BaseCouchBaseRepository<CurrencyConversionRatioData, CurrencyConversionRatioQuery>,
        ICurrencyConversionRatioRepository, ICheckableRepository
    {
        public CurrencyConversionRatioRepository(ICouchBaseClientFactory<CurrencyConversionRatioData> clientFactory,
            CouchBaseConfiguration options, ILog log) : base(clientFactory, options, log)
        {
        }

        public Task<CurrencyConversionRatioData> GetAsync(string id)
        {
            return Client.GetAsync(id);
        }

        protected override string GetOrdering(CurrencyConversionRatioQuery query)
        {
            return query?.SortingFields?.ToString() ?? string.Empty;
        }

        protected override string GetSearchFields(CurrencyConversionRatioQuery query)
        {
            return query?.SearchFields?.ToString() ?? string.Empty;
        }
    }
}
