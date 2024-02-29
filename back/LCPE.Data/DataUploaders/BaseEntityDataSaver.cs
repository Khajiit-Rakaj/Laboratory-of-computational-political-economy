using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders;

public class BaseEntityDataSaver<TModel, TQuery> : IEntityDataSaver
    where TModel : DataEntity
    where TQuery : IQuery
{
    public Type GetEntityType => typeof(TModel);

    private readonly IBaseRepository<TModel, TQuery> repository;

    protected BaseEntityDataSaver(IBaseRepository<TModel, TQuery> repository)
    {
        this.repository = repository;
    }

    public async Task SaveAsync(IEnumerable<object> entities)
    {
        var castedEntities = entities.Select(x => x as TModel).Where(x => x != null);

        await repository.CreateAsync(castedEntities);
    }
}