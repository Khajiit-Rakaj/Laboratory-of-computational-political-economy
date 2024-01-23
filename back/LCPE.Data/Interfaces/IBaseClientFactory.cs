using LCPE.Data.BaseDataEntities;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.Interfaces;

public interface IBaseClientFactory<T, TModel>
    where T : IBaseClient<TModel>
    where TModel : DataEntity
{
    Task<T> CreateAsync(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration,
        ILog log);

    Task<bool> CheckConnection(ConnectionConfiguration connectionConfiguration);
}