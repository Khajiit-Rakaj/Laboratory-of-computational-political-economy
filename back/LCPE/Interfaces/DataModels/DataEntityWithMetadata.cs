using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

public abstract class DataEntityWithMetadata : DataEntity
{
    [PresentableField(DataType.Metadata)]
    public Metadata Metadata { get; set; }

    [ServiceField(DataType.IntValue)]
    public ApprovalStatus ApprovalStatus { get; set; }
}