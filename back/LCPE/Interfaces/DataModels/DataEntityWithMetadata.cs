using LCPE.Attributes;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

public abstract class DataEntityWithMetadata : DataEntity
{
    [PresentableField(DataType.Metadata)]
    public Metadata Metadata { get; set; }
}