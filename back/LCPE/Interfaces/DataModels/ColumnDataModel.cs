using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

public class ColumnDataModel
{
    /// <summary>
    /// Represent visibility of column for external users
    /// </summary>
    public bool External { get; set; }
    
    public string Name { get; set; }

    public DataType DataType { get; set; }

    public string? UnknownValueStab { get; set; }

    public static ColumnDataModel Create(string name, DataType type, bool isExternal, string unknownValueStab = "")
    {
        return new ColumnDataModel
        {
            Name = name,
            DataType = type,
            External = isExternal,
            UnknownValueStab = unknownValueStab
        };
    }
}