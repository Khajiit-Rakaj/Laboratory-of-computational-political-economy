using LCPE.Configurations;
using LCPE.Data.Interfaces;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Data.BaseDataEntities;

public abstract class BaseRepository<TModel>
    where TModel : DataEntity
{
    protected readonly IBaseClient<TModel> Client;
    private readonly string collectionName;
    private readonly ConnectionConfiguration connectionConfiguration;
    private readonly ICouchBaseClientFactory<TModel> clientFactory;

    protected BaseRepository(ICouchBaseClientFactory<TModel> clientFactory, CouchBaseConfiguration options, ILog log)
    {
        collectionName = typeof(TModel).GetCouchBaseRelationCollection();
        this.clientFactory = clientFactory;

        connectionConfiguration = ConnectionConfiguration.Create(options.Server, options.UserName,
            options.Password, options.CouchBaseOptions.DefaultBucket);
        var indexConfiguration = IndexConfiguration.Create(options.CouchBaseOptions.DefaultScope, collectionName);
        Client = this.clientFactory.CreateAsync(connectionConfiguration, indexConfiguration, log).Result;
    }

    public async Task<bool> CheckState()
    {
        var result = await clientFactory.CheckConnection(connectionConfiguration);

        return result;
    }

    public Type GetDataModel => typeof(TModel); 
}