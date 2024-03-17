using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.CorporateFinance}")]
public class CorporationFinancesController : BaseQueryController<CorporationFinancesData, CorporationFinancesQuery>
{
    public CorporationFinancesController(ICorporationFinancesService service) : base(service)
    {
    }
}