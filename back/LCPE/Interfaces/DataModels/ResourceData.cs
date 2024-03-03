using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels
{
    [CouchBaseRelation(DataConstants.Resource)]
    public class ResourceData : DataEntityWithMetadata
    {
        [PresentableField(DataType.IntValue)]
        public int Year { get; set; }

        [PresentableField(DataType.StringArrayValue)]
        public string[] ResourceName { get; set; }

        [PresentableField(DataType.StringArrayValue)]
        public string[] ShortName { get; set; }

        [PresentableField(DataType.StringValue)]
        public string Organization { get; set; }

        [PresentableField(DataType.StringValue)]
        public string OriginCountry { get; set; }

        [PresentableField(DataType.FloatPointValue)]
        public double Amount { get; set; }

        [PresentableField(DataType.StringValue)]
        public string Unit { get; set; }

        [PresentableField(DataType.FloatPointValue)]
        public double Cost { get; set; }

        [PresentableField(DataType.StringValue)]
        public string Currency {  get; set; }
    }
}
