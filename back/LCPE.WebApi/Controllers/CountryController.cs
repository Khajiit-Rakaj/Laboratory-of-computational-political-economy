using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using LCPE.Data.QueryBuilders.Couchbase;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.Country}")]
public class CountryController : Controller
{
    private readonly ICountryService countryService;

    public CountryController(ICountryService countryService)
    {
        this.countryService = countryService;
    }

    [HttpPost]
    [Route("Query")]
    public async Task<IActionResult> Query()
    {
        var queryBuilder = CouchBaseQueryBuilder<CountryQuery>.Create();
        
        var result = await countryService.GetWorkTableViewModel(queryBuilder);

        return Ok(result);
    }
}