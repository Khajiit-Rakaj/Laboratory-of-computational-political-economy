using LCPE.Business.Interfaces.Services;
using LCPE.Business.Interfaces.ViewModels;
using LCPE.Data.Helpers;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class CorporationFinancesService: BaseDataEntityService, ICorporationFinancesService
{
    private readonly ICorporationFinancesRepository corporationFinancesRepository;

    public CorporationFinancesService(ICorporationFinancesRepository corporationFinancesRepository)
    {
        this.corporationFinancesRepository = corporationFinancesRepository;
    }
    
    public async Task<WorkTableViewModel> GetWorkTableViewModel(IQueryBuilder<CorporationFinancesQuery> queryBuilder)
    {
        var data = (await corporationFinancesRepository.SearchAsync(queryBuilder)).ToList();
        var fields = GetFields<CorporationFinances, CorporationFinancesReturnFields>(queryBuilder.Query.ReturnFields).ToList();
        var returnData = DataPreparerHelper.PrepareData(fields, data);

        return WorkTableViewModel.Create(
            nameof(CorporationFinances),
            List.Create(typeof(CorporationFinances).GetCouchBaseRelationCollection()),
            fields,
            returnData);
    }
}