using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class TableService : ITableService
{
    private readonly ITablesRepository repository;

    public TableService(ITablesRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ICollection<TableModel>> GetTablesAsync()
    {
        return await repository.GetTablesAsync();
    }
}