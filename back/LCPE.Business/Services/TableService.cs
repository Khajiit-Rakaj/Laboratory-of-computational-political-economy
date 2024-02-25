using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Extensions;
using LCPE.Interfaces;
using LCPE.Interfaces.DataModels;
using log4net;

namespace LCPE.Business.Services;

public class TableService : ITableService
{
    private readonly ITablesRepository repository;
    private readonly ILog log;
    private readonly IEnumerable<TableModel> entityTableModels;

    public TableService(ITablesRepository repository, IEnumerable<IDataEntity> dataEntities, ILog log)
    {
        this.repository = repository;
        this.log = log;
        entityTableModels = dataEntities.Where(x => !x.IsServiceTable()).Select(x =>
            TableModel.Create(x.GetCouchBaseRelationCollection(), tableDataModel: x.GetColumnDataModels())).ToList();
    }

    public async Task<ICollection<TableModel>> GetTablesAsync()
    {
        var tables = (await repository.GetTablesAsync()).ToList();
        tables.RemoveAll(x => entityTableModels.All(y => x.TableName != y.TableName));

        if (tables.Count != entityTableModels.Count())
        {
            var missingTables = entityTableModels.Select(x => x.TableName).Except(tables.Select(t => t.TableName));

            log.Error($"Missing table(s): {string.Join(", ", missingTables)}");
        }

        var docCount = await repository.GetDocCount(tables.Select(x => x.TableName));
        tables.ForEach(x =>
            {
                x.DocumentCount = docCount[x.TableName];
                x.TableDataModel = entityTableModels.First(y => y.TableName == x.TableName).TableDataModel;
            }
        );

        return tables;
    }
}