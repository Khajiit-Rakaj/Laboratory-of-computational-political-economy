using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces.Repositories;

public interface ITablesRepository : IBaseRepository
{
    Task<ICollection<TableModel>> GetTablesAsync();
}