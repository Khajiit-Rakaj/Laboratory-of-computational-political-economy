using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Interfaces;

namespace LCPE.Business.Interfaces.Services;

public interface IDataEntityService<TQuery> where TQuery : IQuery
{
    Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<TQuery> queryBuilder);
}