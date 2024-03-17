﻿using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository;

public class MetadataSourceRepository : BaseCouchBaseRepository<MetadataSource, MetadataSourceQuery>, IMetadataSourceRepository, ICheckableRepository
{
    public MetadataSourceRepository(ICouchBaseClientFactory<MetadataSource> clientFactory, CouchBaseConfiguration options, ILog log)
        : base(clientFactory, options, log)
    {
    }

    protected override string GetSearchFields(MetadataSourceQuery query)
    {
        var result = string.Empty;
        var searchFields = query.SearchFields;
        if (searchFields == null)
        {
            return result;
        }

        return result;
    }

    protected override string GetOrdering(MetadataSourceQuery query)
    {
        var result = string.Empty;
        var sortingFields = query.SortingFields;
        if (sortingFields == null)
        {
            return result;
        }
        
        return result;
    }

    public Task<MetadataSource> GetAsync(string id)
    {
        return Client.GetAsync(id);
    }
}