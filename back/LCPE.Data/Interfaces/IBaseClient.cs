using Couchbase.KeyValue;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

public interface IBaseClient<TModel>
{
    Task<IEnumerable<TableModel>> GetTablesAsync();

    Task<IDictionary<string, int>> GetDocCountAsync(IEnumerable<string> tables); 
        
    // Todo: в данном случае возвращаем строго типизированный объект, что ломает инверсию надо переделать на более абстрактную реализацию
    Task<TModel?> GetAsync(string id);

    Task<IEnumerable<TModel>> SearchAsync();
    
}