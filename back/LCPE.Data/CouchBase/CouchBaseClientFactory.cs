using System.Collections.Concurrent;
using Couchbase;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;

namespace LCPE.Data.CouchBase;

public class CouchBaseClientFactory : BaseClientFactory<ICouchBaseClient>, ICouchBaseClientFactory
{
    protected override async Task<ICouchBaseClient> CreateClient(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration)
    {
        var options = new ClusterOptions
        {
            UserName = connectionConfiguration.User,
            Password = connectionConfiguration.Password
        };

        var cluster = await Cluster.ConnectAsync($"couchbase://{connectionConfiguration.ConnectionEndpoint}", options);
        var bucket = await cluster.BucketAsync(connectionConfiguration.Bucket);
        var scope = await bucket.ScopeAsync(indexConfiguration.Scope);
        
        var collection = await scope.CollectionAsync(indexConfiguration.Index);
        
        return CouchBaseClient.Create(collection);
    }
}