using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route($"api/{DataConstants.CurrencyConversionRatio}")]
    public class CurrencyConversionRatioController : BaseQueryController<CurrencyConversionRatioQuery>
    {
        public CurrencyConversionRatioController(ICurrencyConversionRatioService currencyConversionRatioService)
            : base(currencyConversionRatioService) { }
    }
}
