using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces;
using LCPE.Data.QueryBuilders.Couchbase;
using LCPE.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseQueryController<TModel, TQuery> : Controller
    where TModel : DataEntity
    where TQuery : IQuery
{
    private readonly IDataEntityService<TModel, TQuery> service;

    public BaseQueryController(IDataEntityService<TModel, TQuery> service)
    {
        this.service = service;
    }
    
    [HttpPost]
    [Route("Query")]
    public async Task<IActionResult> Query()
    {
        var queryBuilder = CouchBaseQueryBuilder<TQuery>.Create();
        
        var result = await service.GetWorkTableViewModel(queryBuilder);

        return Ok(result);
    }
}