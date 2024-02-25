using Couchbase;
using Couchbase.KeyValue;
using LCPE.Constants;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Helpers;
using LCPE.Data.Interfaces;
using LCPE.Interfaces.DataModels;
using LCPE.Logging;
using log4net;
using Microsoft.Extensions.Logging;

namespace LCPE.Data.CouchBase;

public class CouchBaseClientFactory<TModel> : BaseClientFactory<ICouchBaseClient<TModel>>,
    ICouchBaseClientFactory<TModel>
    where TModel : DataEntity
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
        IScope scope = default;
        ICouchbaseCollection collection = default;
        try
        {
            var bucket = await cluster.BucketAsync(connectionConfiguration.Bucket);

            scope = await bucket.ScopeAsync(indexConfiguration.Scope);
            collection = await scope.CollectionAsync(indexConfiguration.Index);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return CouchBaseClient<TModel>.Create(collection, scope, cluster, connectionConfiguration, indexConfiguration, log);
    }

    protected override async Task<DiagnosticResultsType> CheckConnectionToDataBase(
        ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration)
    {
        return await CouchBaseCheckHelper.Check(connectionConfiguration, indexConfiguration);
    }
}