using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using LCPE.Data.QueryBuilders.Couchbase;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.CorporateFinance}")]
public class CorporationFinancesController : Controller
{
    private readonly ICorporationFinancesService corporationFinancesService;

    public CorporationFinancesController(ICorporationFinancesService corporationFinancesService)
    {
        this.corporationFinancesService = corporationFinancesService;
    }
    
    [HttpPost]
    [Route("Query")]
    public async Task<IActionResult> Query()
    {
        var queryBuilder = CouchBaseQueryBuilder<CorporationFinancesQuery>.Create();
        
        var result = await corporationFinancesService.GetWorkTableViewModel(queryBuilder);
        
        return Ok(result);
    }
}