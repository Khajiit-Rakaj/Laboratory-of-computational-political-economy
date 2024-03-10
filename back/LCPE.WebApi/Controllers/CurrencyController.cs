using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route($"api/{DataConstants.Currency}")]
    public class CurrencyController : BaseQueryController<CurrencyQuery>
    {
        public CurrencyController(ICurrencyService currencyService) : base(currencyService) { }
    }
}
