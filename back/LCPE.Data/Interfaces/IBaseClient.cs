using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

public interface IBaseClient
{
    Task<IEnumerable<TableModel>> GetTablesAsync();

}