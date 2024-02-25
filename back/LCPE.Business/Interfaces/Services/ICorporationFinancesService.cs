using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Interfaces;
using LCPE.Data.Queries;

namespace LCPE.Business.Interfaces.Services;

public interface ICorporationFinancesService
{
    Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<CorporationFinancesQuery> queryBuilder);
}