using Couchbase;
using Couchbase.Diagnostics;
using Couchbase.KeyValue;
using Couchbase.Management.Buckets;
using Couchbase.Management.Collections;

namespace LCPE.Data.Helpers;

public static class CouchBaseInfrastructureHelper
{
    private static int bucketWaitDelay = 20000;
    private static int defaultDelay = 1000;

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
            await Task.Delay(TimeSpan.FromMilliseconds(defaultDelay));
            await cluster.WaitUntilReadyAsync(TimeSpan.FromMilliseconds(defaultDelay), new WaitUntilReadyOptions(){});
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
            await cbBucket.WaitUntilReadyAsync(TimeSpan.FromMilliseconds(defaultDelay)).WaitAsync(TimeSpan.FromMilliseconds(defaultDelay));
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

        if (cbScopes.Exists(x => x.Name == scopeName) && cbScopes.Find(x => x.Name == scopeName).Collections
                .All(x => x.Name != indexName))
        {
            await collectionManager.CreateCollectionAsync(new CollectionSpec(scopeName, indexName));
            await cbBucket.WaitUntilReadyAsync(TimeSpan.FromMilliseconds(defaultDelay)).WaitAsync(TimeSpan.FromMilliseconds(defaultDelay));
            cbCollection = await cbBucket.CollectionAsync(indexName);
        }

        return cbCollection;
    }
}