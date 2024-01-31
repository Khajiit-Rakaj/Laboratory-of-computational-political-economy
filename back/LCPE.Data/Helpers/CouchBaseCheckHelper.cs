using Couchbase;
using LCPE.Constants;
using LCPE.Data.BaseDataEntities;

namespace LCPE.Data.Helpers;

public static class CouchBaseCheckHelper
{
    public static async Task<DiagnosticResultsType> Check(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration)
    {
        var options = new ClusterOptions
        {
            UserName = connectionConfiguration.User,
            Password = connectionConfiguration.Password
        };

        ICluster cluster;
        IBucket bucket;

        try
        {
            cluster = await Cluster.ConnectAsync($"couchbase://{connectionConfiguration.ConnectionEndpoint}", options);
        }
        catch (Exception)
        {
            return DiagnosticResultsType.UnableToConnect;
        }

        try
        {
            bucket = await cluster.BucketAsync(connectionConfiguration.Bucket);
        }
        catch (Exception)
        {
            return DiagnosticResultsType.MissingBucket;
        }

        var collections = bucket.Collections;
        var allScopes = await collections.GetAllScopesAsync();
        var scope = allScopes.FirstOrDefault(x => x.Name == indexConfiguration.Scope);

        if (scope == default)
        {
            return DiagnosticResultsType.MissingScope;
        }

        var collection = scope.Collections.FirstOrDefault(x => x.Name == indexConfiguration.Index);
        
        if (collection == default)
        {
            return DiagnosticResultsType.MissingCollection;
        }

        await cluster.DisposeAsync();

        return DiagnosticResultsType.Success;
    }
}