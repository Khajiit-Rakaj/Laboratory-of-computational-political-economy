using Couchbase;
using Couchbase.KeyValue;
using Couchbase.Management.Buckets;
using Couchbase.Management.Collections;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Helpers;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using LCPE.Helpers;
using LCPE.Interfaces.DataModels;
using log4net;
using Newtonsoft.Json.Linq;

namespace LCPE.Data.CouchBase;

public class CouchBaseClient<TModel> : ICouchBaseClient<TModel>
    where TModel : DataEntity
{
    public string TablePlaceHolder => "<table_name>";

    private readonly ICouchbaseCollection collection;
    private readonly IScope scope;
    private readonly ICluster cluster;
    private readonly ConnectionConfiguration connectionConfiguration;
    private readonly IndexConfiguration indexConfiguration;
    private readonly ILog log;
    private readonly int limitQuota = 128;

    private CouchBaseClient(ICouchbaseCollection collection, IScope scope, ICluster cluster,
        ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration, ILog log)
    {
        this.collection = collection;
        this.scope = scope;
        this.cluster = cluster;
        this.connectionConfiguration = connectionConfiguration;
        this.indexConfiguration = indexConfiguration;
        this.log = log;
    }

    // Todo: добавить синхронизацию с существующими сервисами, дабы не показать того чего не надо (сделать это на более высоком уровне)
    public async Task<IEnumerable<TableModel>> GetTablesAsync()
    {
        var collections = (await collection.Scope.Bucket.Collections.GetAllScopesAsync())
            .FirstOrDefault(x => x.Name == collection.Scope.Name)?.Collections;
        var result = collections?.Select(x => TableModel.Create(x.Name));

        return result ?? List.Create<TableModel>();
    }

    public async Task<IDictionary<string, int>> GetDocCountAsync(IEnumerable<string> tables)
    {
        var queries = "Select " + string.Join(",",
            tables.Select(x => $"(Select '{x}' as Table, Count(*) as Count from {x})"));

        var result = await scope.QueryAsync<dynamic>(queries);
        var count = ((await result.Rows.ToListAsync())[0] as JObject)?.Children().SelectMany(x => x.Children()[0])
                    .ToDictionary(
                        k => JObject.FromObject(k)["Table"]?.ToString() ?? string.Empty,
                        v => int.Parse(JObject.FromObject(v)["Count"]?.ToString() ?? "0")) ??
                    new Dictionary<string, int>();

        return count;
    }

    public async Task<TModel?> GetAsync(string id)
    {
        try
        {
            var result = await collection.GetAsync(id);

            return result.ContentAs<TModel>();
        }
        catch (Exception e)
        {
            log.Error($"Failed to fetch Id {id} from {typeof(TModel).Name}", e);
        }

        return default;
    }

    public async Task<IEnumerable<TModel>> SearchAsync(object queryObject)
    {
        if (queryObject is not string query)
        {
            log.Error($"Failed to stringify query");
            return List.Create<TModel>();
        }

        ValidateQuery(query);
        query = AppendTargetTableName(query);

        try
        {
            var dynamicResult = await scope.QueryAsync<dynamic>(query);

            var result = await dynamicResult
                .Select(x => (TModel)JObject.FromObject(x)[collection.Name].ToObject<TModel>())
                .ToListAsync();

            return result;
        }
        catch (Exception e)
        {
            log.Error($"Failed to fetch data with query: {query}");
            log.Error(e);
            throw;
        }
    }

    public async Task CreateAsync(IEnumerable<TModel> entities)
    {
        var entitiesWithId = entities.Select(x =>
        {
            x.Id ??= Guid.NewGuid().ToString();
            return x;
        });
        
        await entitiesWithId.ToAsyncEnumerable()
            .ForEachAwaitAsync(entity => collection.InsertAsync(entity.Id ?? Guid.NewGuid().ToString(), entity));
    }

    public async Task<bool> CreateCollectionAsync()
    {
        var cbBucket =
            await CouchBaseInfrastructureHelper.GetOrCreateBucketAsync(cluster, connectionConfiguration.Bucket,
                limitQuota);
        var cbScope = await CouchBaseInfrastructureHelper.GetOrCreateScopeAsync(cbBucket, indexConfiguration.Scope);
        var cbCollection =
            await CouchBaseInfrastructureHelper.GetOrCreateCollectionAsync(cbBucket, indexConfiguration.Scope,
                indexConfiguration.Index);

        if (cbCollection == default)
        {
            log.Error($"failed to create {connectionConfiguration.ConnectionEndpoint}" +
                      $".{connectionConfiguration.Bucket}.{indexConfiguration.Scope}.{indexConfiguration.Index}");
        }

        return cbCollection != default;
    }

    private string AppendTargetTableName(string query)
    {
        return query.Replace(TablePlaceHolder, collection.Name);
    }

    private static void ValidateQuery(string? query)
    {
        if (string.IsNullOrEmpty(query))
        {
            throw new ArgumentException("Query can't be null");
        }
    }

    #region Static constructor

    public static CouchBaseClient<TModel> Create(ICouchbaseCollection collection, IScope scope, ICluster cluster,
        ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration, ILog log)
    {
        return new CouchBaseClient<TModel>(collection, scope, cluster, connectionConfiguration, indexConfiguration,
            log);
    }

    #endregion Static constructor
}