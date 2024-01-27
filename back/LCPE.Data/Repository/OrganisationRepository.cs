using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository;

public class OrganisationRepository : BaseCouchBaseRepository<Organisation, OrganisationQuery>, IOrganisationRepository,
    ICheckableRepository
{
    public OrganisationRepository(ICouchBaseClientFactory<Organisation> clientFactory, CouchBaseConfiguration options,
        ILog log) : base(clientFactory, options, log)
    {
    }

    public Task<Organisation> GetAsync(string id)
    {
        return Client.GetAsync(id);
    }

    protected override string GetSearchFields(OrganisationQuery query)
    {
        var result = string.Empty;
        var searchFields = query.SearchFields;
        if (searchFields == null) return result;

        return result;
    }

    protected override string GetOrdering(OrganisationQuery query)
    {
        var result = string.Empty;
        var sortingFields = query.SortingFields;
        if (sortingFields == null) return result;

        return result;
    }
}