using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces.Repositories;

public interface ITablesRepository
{
    Task<ICollection<TableModel>> GetTablesAsync();

    Task<IDictionary<string, int>> GetDocCount(IEnumerable<string> tables);
}