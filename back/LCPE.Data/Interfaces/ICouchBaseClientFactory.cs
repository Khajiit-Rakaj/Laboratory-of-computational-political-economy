using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

public interface ICouchBaseClientFactory<TModel> : IBaseClientFactory<ICouchBaseClient<TModel>, TModel>
    where TModel : DataEntity
{
    
}