using LCPE.Attributes;
using LCPE.Constants;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.Tables)]
public class TableModel
{
    public string TableName { get; set; }

    public int DocumentCount { get; set; }

    public TableDataModel TableDataModel { get; set; }

    public static TableModel Create(string tableName, int documentCount = 0, TableDataModel tableDataModel = null)
    {
        return new TableModel
        {
            TableName = tableName,
            DocumentCount = documentCount,
            TableDataModel = tableDataModel ?? TableDataModel.Create()
        };
    }
}