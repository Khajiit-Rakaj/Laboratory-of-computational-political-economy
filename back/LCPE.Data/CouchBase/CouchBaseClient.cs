using Couchbase.KeyValue;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;
using log4net;
using Newtonsoft.Json.Linq;
using NotImplementedException = System.NotImplementedException;

namespace LCPE.Data.CouchBase;

public class CouchBaseClient<TModel> : ICouchBaseClient<TModel>
    where TModel : DataEntity
{
    public string TablePlaceHolder => "<table_name>";
    
    private readonly ICouchbaseCollection collection;
    private readonly IScope scope;
    private readonly ILog log;

    private CouchBaseClient(ICouchbaseCollection collection, IScope scope, ILog log)
    {
        this.collection = collection;
        this.scope = scope;
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

    public static CouchBaseClient<TModel> Create(ICouchbaseCollection collection, IScope scope, ILog log)
    {
        return new CouchBaseClient<TModel>(collection, scope, log);
    }

    #endregion Static constructor
}