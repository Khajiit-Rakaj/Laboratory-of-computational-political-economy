using LCPE.Data.BaseDataEntities;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Interfaces.ViewModels;

public class WorkTableViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<string> SourceTables { get; set; }

    public IEnumerable<ColumnDataModel> Fields { get; set; }

    public IEnumerable<IEnumerable<NameValuePair>> Values { get; set; }

    public IEnumerable<string> Joins { get; set; }

    public static WorkTableViewModel Create(
        string tableName,
        IEnumerable<string> sourceTables,
        IEnumerable<ColumnDataModel> fields,
        IEnumerable<IEnumerable<NameValuePair>> values)
    {
        return new WorkTableViewModel
        {
            Name = tableName,
            SourceTables = sourceTables,
            Fields = fields,
            Values = values,
        };
    }
}