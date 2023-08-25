using LCPE.Extensions;

namespace LCPE.Interfaces.DataModels;

public class TableDataModel
{
    public ICollection<ColumnDataModel> Columns { get; set; }

    public static TableDataModel Create(ICollection<ColumnDataModel> columns = null)
    {
        return new TableDataModel { Columns = columns ?? List.Create<ColumnDataModel>() };
    }
}