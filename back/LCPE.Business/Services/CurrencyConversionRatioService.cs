using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services
{
    public class CurrencyConversionRatioService : BaseDataEntityService<CurrencyConversionRatioData, CurrencyConversionRatioQuery,
        CurrencyConversionRatioReturnFields, CurrencyConversionRatioSearchFields, CurrencyConversionRatioSortingFields>, ICurrencyConversionRatioService
    {
        public CurrencyConversionRatioService(ICurrencyConversionRatioRepository currencyConversionRatioRepository)
            : base(currencyConversionRatioRepository) { }
    }
}
