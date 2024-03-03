using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels
{
    [CouchBaseRelation(DataConstants.Currency)]
    public class CurrencyData : DataEntity
    {
        [PresentableField(DataType.StringValue)]
        public string Name { get; set; }

        [PresentableField(DataType.StringValue)]
        public string Sign { get; set; }
    }
}
