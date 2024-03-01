using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces;
using LCPE.Data.QueryBuilders.Couchbase;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseQueryController<TQuery> : Controller
    where TQuery : IQuery
{
    private readonly IDataEntityService<TQuery> countryService;

    public BaseQueryController(IDataEntityService<TQuery> countryService)
    {
        this.countryService = countryService;
    }
    
    [HttpPost]
    [Route("Query")]
    public async Task<IActionResult> Query()
    {
        var queryBuilder = CouchBaseQueryBuilder<TQuery>.Create();
        
        var result = await countryService.GetWorkTableViewModel(queryBuilder);

        return Ok(result);
    }
}