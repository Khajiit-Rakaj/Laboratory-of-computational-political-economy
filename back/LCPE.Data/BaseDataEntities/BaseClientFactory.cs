using System.Collections.Concurrent;
using LCPE.Data.Interfaces;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseClientFactory<T> : IBaseClientFactory<T> where T: IBaseClient
{
    private readonly ConcurrentDictionary<string, T> clients = new();
    
    public async Task<T> CreateAsync(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration)
    {
        var key = connectionConfiguration.Bucket + indexConfiguration.Scope + indexConfiguration.Index;

        return clients.GetOrAdd(key, await CreateClient(connectionConfiguration, indexConfiguration));
    }

    protected abstract Task<T> CreateClient(ConnectionConfiguration connectionConfiguration,
        IndexConfiguration indexConfiguration);
}