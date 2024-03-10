using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository
{
    public class ResourceRepository : BaseCouchBaseRepository<ResourceData, ResourceQuery>, IResourceRepository, ICheckableRepository
    {
        public ResourceRepository(ICouchBaseClientFactory<ResourceData> clientFactory, CouchBaseConfiguration options, ILog log)
            : base(clientFactory, options, log)
        {
        }

        public Task<ResourceData> GetAsync(string id)
        {
            return Client.GetAsync(id);
        }

        protected override string GetOrdering(ResourceQuery query)
        {
            return query?.SortingFields?.ToString() ?? string.Empty;
        }

        protected override string GetSearchFields(ResourceQuery query)
        {
            return query?.SearchFields?.ToString() ?? string.Empty;
        }
    }
}
