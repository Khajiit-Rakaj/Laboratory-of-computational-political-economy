using Couchbase;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Logging;
using log4net;
using Microsoft.Extensions.Logging;

namespace LCPE.Data.CouchBase;

public class CouchBaseClientFactory<TModel> : BaseClientFactory<ICouchBaseClient<TModel>, TModel>,
    ICouchBaseClientFactory<TModel>
{
    protected override async Task<ICouchBaseClient<TModel>> CreateClient(
        ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration, ILog log)
    {
        var loggerFactory = new LoggerFactory();
        loggerFactory.AddProvider(new Log4NetProvider(log));
        
        var options = new ClusterOptions
        {
            UserName = connectionConfiguration.User,
            Password = connectionConfiguration.Password
        }.WithLogging(loggerFactory);

        var cluster = await Cluster.ConnectAsync($"couchbase://{connectionConfiguration.ConnectionEndpoint}", options);
        var bucket = await cluster.BucketAsync(connectionConfiguration.Bucket);
        
        var scope = await bucket.ScopeAsync(indexConfiguration.Scope);
        var collection = await scope.CollectionAsync(indexConfiguration.Index);

        return CouchBaseClient<TModel>.Create(collection, scope, log);
    }
}