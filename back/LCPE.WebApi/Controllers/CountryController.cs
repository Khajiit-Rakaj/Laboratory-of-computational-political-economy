using LCPE.Business.Interfaces.Services;
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
        var result = await countryService.SearchAsync();

        return Ok(result);
    }
}