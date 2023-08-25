using Couchbase.KeyValue;
using Couchbase.Management.Collections;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.CouchBase;

public class CouchBaseClient : ICouchBaseClient
{
    private readonly ICouchbaseCollection collection;

    private CouchBaseClient(ICouchbaseCollection collection)
    {
        this.collection = collection;
    }

    // Todo: добавить синхронизацию с существующими сервисами, дабы не показать того чего не надо (сделать это на более высоком уровне)
    public async Task<IEnumerable<TableModel>> GetTablesAsync()
    {
        var collections = (await collection.Scope.Bucket.Collections.GetAllScopesAsync())
            .FirstOrDefault(x => x.Name == collection.Scope.Name)?.Collections;
        var result = collections?.Select(x => TableModel.Create(x.Name));

        return result ?? List.Create<TableModel>();
    }

    public static CouchBaseClient Create(ICouchbaseCollection collection)
    {
        return new CouchBaseClient(collection);
    }
}