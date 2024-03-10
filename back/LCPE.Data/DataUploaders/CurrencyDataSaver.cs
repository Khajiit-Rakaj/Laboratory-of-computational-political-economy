using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders
{
    public class CurrencyDataSaver : BaseEntityDataSaver<CurrencyData, CurrencyQuery>
    {
        public CurrencyDataSaver(ICurrencyRepository currencyRepository) : base(currencyRepository) { }
    }
}
