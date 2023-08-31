namespace LCPE.Data.Interfaces;

public interface ICouchBaseClientFactory<TModel> : IBaseClientFactory<ICouchBaseClient<TModel>, TModel>
{
    
}