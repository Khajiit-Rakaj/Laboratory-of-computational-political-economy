using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces.Repositories
{
    public interface ICurrencyRepository : IBaseRepository<CurrencyData, CurrencyQuery>
    {
    }
}
