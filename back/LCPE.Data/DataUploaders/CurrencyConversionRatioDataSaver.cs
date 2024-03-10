using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders
{
    public class CurrencyConversionRatioDataSaver : BaseEntityDataSaver<CurrencyConversionRatioData, CurrencyConversionRatioQuery>
    {
        public CurrencyConversionRatioDataSaver(ICurrencyConversionRatioRepository repository) : base(repository) { }
    }
}
