using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.Services;

public interface ITableService : IService
{
    Task<ICollection<TableModel>> GetTablesAsync();
}