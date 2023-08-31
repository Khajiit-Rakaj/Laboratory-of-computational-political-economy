using System.Reflection;
using LCPE.Attributes;
using LCPE.Configurations;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using log4net;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseRepository<T> where T : class
{
    protected IBaseClient<T> client;

    protected BaseRepository(ICouchBaseClientFactory<T> clientFactory, CouchBaseConfiguration options, ILog log)
    {
        var collectionName = typeof(T).GetCouchBaseRelationCollection();
        
        var connectionConfiguration = ConnectionConfiguration.Create(options.Server, options.UserName,
            options.Password, options.CouchBaseOptions.DefaultBucket);
        var indexConfiguration = IndexConfiguration.Create(options.CouchBaseOptions.DefaultScope);
        indexConfiguration.Index = collectionName;
        client = clientFactory.CreateAsync(connectionConfiguration, indexConfiguration, log).Result;
    }
}