using Couchbase;
using Couchbase.Diagnostics;
using Couchbase.KeyValue;
using Couchbase.Management.Buckets;
using Couchbase.Management.Collections;
using Couchbase.Management.Query;

namespace LCPE.Data.Helpers;

public static class CouchBaseInfrastructureHelper
{
    private const int DefaultDelay = 1000;

    public static async Task<IBucket> GetOrCreateBucketAsync(ICluster cluster, string buketName, int bucketSize)
    {
        IBucket cbBucket;

        var bucketManager = cluster.Buckets;
        var cbBuckets = await bucketManager.GetAllBucketsAsync();

        if (cbBuckets.All(x => x.Key != buketName))
        {
            await bucketManager.CreateBucketAsync(new BucketSettings()
            {
                Name = buketName,
                BucketType = BucketType.Couchbase,
                RamQuotaMB = bucketSize
            });
            await Task.Delay(TimeSpan.FromMilliseconds(DefaultDelay));
            await cluster.WaitUntilReadyAsync(TimeSpan.FromMilliseconds(DefaultDelay), new WaitUntilReadyOptions(){});
        }

        cbBuckets = await bucketManager.GetAllBucketsAsync();
        cbBucket = await cluster.BucketAsync(buketName);
        var pingResults = await cbBucket.PingAsync();

        return cbBucket;
    }

    public static async Task<IScope> GetOrCreateScopeAsync(IBucket cbBucket, string scopeName)
    {
        IScope cbScope;
        var collectionManager = cbBucket.Collections;
        var cbScopes = await collectionManager.GetAllScopesAsync();

        if (cbScopes.All(x => x.Name != scopeName))
        {
            await collectionManager.CreateScopeAsync(scopeName);
            await cbBucket.WaitUntilReadyAsync(TimeSpan.FromMilliseconds(DefaultDelay)).WaitAsync(TimeSpan.FromMilliseconds(DefaultDelay));
        }

        cbScope = await cbBucket.ScopeAsync(scopeName);

        return cbScope;
    }

    public static async Task<ICouchbaseCollection> GetOrCreateCollectionAsync(IBucket cbBucket, string scopeName,
        string indexName)
    {
        ICouchbaseCollection cbCollection = default;
        var collectionManager = cbBucket.Collections;
        var cbScopes = (await collectionManager.GetAllScopesAsync()).ToList();

        if (cbScopes.Exists(x => x.Name == scopeName) && 
            cbScopes.Find(x => x.Name == scopeName).Collections.All(x => x.Name != indexName))
        {
            await collectionManager.CreateCollectionAsync(new CollectionSpec(scopeName, indexName));
            await Task.Delay(TimeSpan.FromMilliseconds(DefaultDelay));
            cbCollection = await cbBucket.CollectionAsync(indexName);
            await cbCollection.QueryIndexes.CreatePrimaryIndexAsync(new CreatePrimaryQueryIndexOptions());
        }

        return cbCollection;
    }
}