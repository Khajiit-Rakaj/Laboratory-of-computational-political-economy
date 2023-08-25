using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;
using Microsoft.Extensions.Options;

namespace LCPE.Data.Repository;

public class TableRepository : BaseRepository<TableModel>, ITablesRepository
{
    public TableRepository(ICouchBaseClientFactory clientFactory, CouchBaseConfiguration options)
        : base(clientFactory, options)
    {
    }


    public async Task<ICollection<TableModel>> GetTablesAsync()
    {
        var result = (await client.GetTablesAsync()).ToList();

        return result;
    }
}