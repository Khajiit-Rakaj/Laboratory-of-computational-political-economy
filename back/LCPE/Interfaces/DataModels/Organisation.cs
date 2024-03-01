using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.Organisation)]
public class Organisation : DataEntity
{
    [PresentableField(DataType.StringValue)]
    public string Name { get; set; }

    [PresentableField(DataType.StringValue)]
    public string ShortName { get; set; }
}