using LCPE.Interfaces.Enums;

namespace LCPE.Attributes;

public class PresentableFieldAttribute : Attribute
{
    public DataType DataType { get; }

    public PresentableFieldAttribute(DataType dataType)
    {
        DataType = dataType;
    }
}