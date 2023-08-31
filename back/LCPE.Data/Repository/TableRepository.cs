using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Repository;

public class TableRepository : BaseRepository<TableModel>, ITablesRepository
{
    public TableRepository(ICouchBaseClientFactory<TableModel> clientFactory, CouchBaseConfiguration options, ILog log)
        : base(clientFactory, options, log)
    {
    }

    public async Task<ICollection<TableModel>> GetTablesAsync()
    {
        var result = (await client.GetTablesAsync()).ToList();

        return result;
    }

    public Task<IDictionary<string, int>> GetDocCount(IEnumerable<string> tables)
    {
        var result = client.GetDocCountAsync(tables);
        
        return result;
    }
}