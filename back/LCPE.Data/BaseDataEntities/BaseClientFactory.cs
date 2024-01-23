using System.Collections.Concurrent;
using LCPE.Data.Interfaces;
using log4net;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseClientFactory<T>
{
    private readonly ConcurrentDictionary<string, T> clients = new();
    
    public async Task<T> CreateAsync(ConnectionConfiguration connectionConfiguration, IndexConfiguration indexConfiguration, ILog log)
    {
        var key = connectionConfiguration.Bucket + indexConfiguration.Scope + indexConfiguration.Index;

        return clients.GetOrAdd(key, await CreateClient(connectionConfiguration, indexConfiguration, log));
    }

    protected abstract Task<T> CreateClient(ConnectionConfiguration connectionConfiguration,
        IndexConfiguration indexConfiguration, ILog log);

    public async Task<bool> CheckConnection(ConnectionConfiguration connectionConfiguration)
    {
        var result = await CheckConnectionToDataBase(connectionConfiguration);
        
        return result;
    }

    protected abstract Task<bool> CheckConnectionToDataBase(ConnectionConfiguration connectionConfiguration);
}