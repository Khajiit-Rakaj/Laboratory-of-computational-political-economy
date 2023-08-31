using LCPE.Interfaces.Enums;

namespace LCPE.Attributes;

public class ServiceFieldAttribute : Attribute
{
    public DataType DataType { get; }

    public ServiceFieldAttribute(DataType dataType)
    {
        DataType = dataType;
    }
}