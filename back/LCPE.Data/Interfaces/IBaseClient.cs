using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

public interface IBaseClient<TModel>
    where TModel : DataEntity
{
    public string TablePlaceHolder { get; }
    
    Task<IEnumerable<TableModel>> GetTablesAsync();

    Task<IDictionary<string, int>> GetDocCountAsync(IEnumerable<string> tables); 
        
    Task<TModel?> GetAsync(string id);

    Task<IEnumerable<TModel>> SearchAsync(object queryObject);

    Task CreateAsync(IEnumerable<TModel> entities);

    Task<bool> CreateCollectionAsync();
}