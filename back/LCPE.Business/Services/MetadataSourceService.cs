﻿using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class MetadataSourceService : BaseDataEntityService<MetadataSource, MetadataSourceQuery, MetadataSourceReturnFields,
    MetadataSourceSearchFields, MetadataSourceSortingFields>, IMetadataSourceService
{
    public MetadataSourceService(IMetadataSourceRepository repository) : base(repository)
    {
    }
}