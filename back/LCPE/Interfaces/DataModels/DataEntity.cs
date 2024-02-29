using LCPE.Attributes;
using DataType = LCPE.Interfaces.Enums.DataType;

namespace LCPE.Interfaces.DataModels;

public abstract class DataEntity : IDataEntity
{
    [ServiceField(DataType.StringValue)]
    public string? Id { get; set; }
}