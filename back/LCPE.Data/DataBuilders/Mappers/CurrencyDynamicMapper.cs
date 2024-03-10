using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataBuilders.Mappers
{
    public class CurrencyDynamicMapper : BaseDynamicMapper<CurrencyData>
    {
        protected override void CreateMapping(IDictionary<string, string> mapping)
        {
            Map(c => c.Id).Name(mapping[nameof(CurrencyData.Name)]);
            Map(c => c.Name).Name(mapping[nameof(CurrencyData.Name)]);
            Map(c => c.Sign).Name(mapping[nameof(CurrencyData.Sign)]);
        }
    }
}
