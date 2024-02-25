using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces.Repositories;

public interface IBaseRepository<TModel, TQuery>
    where TModel : DataEntity
    where TQuery : IQuery
{
    Task<TModel> GetAsync(string id);
    
    Task<IEnumerable<TModel>> SearchAsync(IQueryBuilder<TQuery> queryBuilder);
}