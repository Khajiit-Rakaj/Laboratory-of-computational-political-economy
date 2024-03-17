using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Interfaces;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.Services;

public interface IDataEntityService<TModel, TQuery> 
    where TModel : DataEntity
    where TQuery : IQuery
{
    Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<TQuery> queryBuilder);

    Task<TModel?> GetAsync(string id);
}