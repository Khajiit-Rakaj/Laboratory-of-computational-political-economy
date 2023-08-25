using LCPE.Data.BaseDataEntities;
using LCPE.Data.CouchBase;

namespace LCPE.Data.Interfaces;

public interface IBaseClientFactory<T> where T : IBaseClient
{
    Task<T> CreateAsync(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration);
}