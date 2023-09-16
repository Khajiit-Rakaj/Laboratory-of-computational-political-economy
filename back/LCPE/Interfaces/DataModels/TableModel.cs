using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Extensions;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.Tables)]
[ServiceTable]
public class TableModel : DataEntity
{
    public string TableName { get; set; }

    public int DocumentCount { get; set; }

    public ICollection<ColumnDataModel> TableDataModel { get; set; }

    public static TableModel Create(string tableName, int documentCount = 0, ICollection<ColumnDataModel> tableDataModel = null)
    {
        return new TableModel
        {
            TableName = tableName,
            DocumentCount = documentCount,
            TableDataModel = tableDataModel ?? List.Create<ColumnDataModel>()
        };
    }
}