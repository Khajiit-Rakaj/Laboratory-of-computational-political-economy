using System.ComponentModel.DataAnnotations;

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
}