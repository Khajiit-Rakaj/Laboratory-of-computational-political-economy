using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository
{
    public class ProductRepository : BaseCouchBaseRepository<ProductData, ProductQuery>, IProductRepository, ICheckableRepository
    {
        public ProductRepository(ICouchBaseClientFactory<ProductData> clientFactory, CouchBaseConfiguration options, ILog log)
            : base(clientFactory, options, log)
        {
        }

        public Task<ProductData> GetAsync(string id)
        {
            return Client.GetAsync(id);
        }

        protected override string GetOrdering(ProductQuery query)
        {
            return query?.SortingFields?.ToString() ?? string.Empty;
        }

        protected override string GetSearchFields(ProductQuery query)
        {
            return query?.SearchFields?.ToString() ?? string.Empty;
        }
    }
}
