using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository
{
    public class CurrencyRepository : BaseCouchBaseRepository<CurrencyData, CurrencyQuery>,
    ICurrencyRepository, ICheckableRepository
    {
        public CurrencyRepository(ICouchBaseClientFactory<CurrencyData> clientFactory, CouchBaseConfiguration options, ILog log)
            : base(clientFactory, options, log)
        {
        }

        public Task<CurrencyData> GetAsync(string id)
        {
            return Client.GetAsync(id);
        }

        protected override string GetOrdering(CurrencyQuery query)
        {
            return query?.SortingFields?.Name.ToString() ?? string.Empty;
        }

        protected override string GetSearchFields(CurrencyQuery query)
        {
            return query?.SearchFields?.ToString() ?? string.Empty;
        }
    }
}
