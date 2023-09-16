using LCPE.Business.Interfaces.Services;
using LCPE.Data.Queries;
using LCPE.Data.QueryBuilders.Couchbase;
using LCPE.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/Country")]
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
        
        var result = await countryService.SearchAsync(queryBuilder);

        return Ok(result);
    }
}