using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.Country}")]
public class CountryController : BaseQueryController<CountryQuery>
{
    public CountryController(ICountryService service): base(service)
    {
    }
}