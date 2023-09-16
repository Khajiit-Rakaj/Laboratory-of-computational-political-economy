using LCPE.Configurations;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseRepository<TModel>
    where TModel : DataEntity
{
    protected IBaseClient<TModel> client;

    protected BaseRepository(ICouchBaseClientFactory<TModel> clientFactory, CouchBaseConfiguration options, ILog log)
    {
        var collectionName = typeof(TModel).GetCouchBaseRelationCollection();

        var connectionConfiguration = ConnectionConfiguration.Create(options.Server, options.UserName,
            options.Password, options.CouchBaseOptions.DefaultBucket);
        var indexConfiguration = IndexConfiguration.Create(options.CouchBaseOptions.DefaultScope, collectionName);
        client = clientFactory.CreateAsync(connectionConfiguration, indexConfiguration, log).Result;
    }
}