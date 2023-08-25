using System.Reflection;
using LCPE.Attributes;
using LCPE.Configurations;
using LCPE.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseRepository<T> where T : class
{
    protected IBaseClient client;

    protected BaseRepository(ICouchBaseClientFactory clientFactory, CouchBaseConfiguration options)
    {
        var collectionName = typeof(T).GetCustomAttribute<CouchBaseRelationAttribute>()?.CollectionName ?? string.Empty;
        
        var connectionConfiguration = ConnectionConfiguration.Create(options.Server, options.UserName,
            options.Password, options.CouchBaseOptions.DefaultBucket);
        var indexConfiguration = IndexConfiguration.Create(options.CouchBaseOptions.DefaultScope);
        indexConfiguration.Index = collectionName;
        client = clientFactory.CreateAsync(connectionConfiguration, indexConfiguration).Result;
    }
}