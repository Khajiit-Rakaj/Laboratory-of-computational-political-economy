using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.MetadataSource)]
public class MetadataSource : DataEntity
{
    [PresentableField(DataType.StringValue)]
    public string Name { get; set; }
    
    [PresentableField(DataType.StringValue)]
    public string Description { get; set; }
    
    [PresentableField(DataType.IntValue)]
    public ReliabilityLevel ReliabilityLevel { get; set; }
}