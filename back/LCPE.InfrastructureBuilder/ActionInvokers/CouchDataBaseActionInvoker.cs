using InfrastructureBuilder.Entities;
using LCPE.Constants;
using LCPE.Data.Interfaces.Repositories;
using LCPE.Extensions;
using LCPE.Helpers;
using LCPE.Interfaces;

namespace InfrastructureBuilder.ActionInvokers;

public class CouchDataBaseActionInvoker : ActionInvokerWithHelp, IActionInvoker
{
    private readonly List<ICheckableRepository> checkableRepositories;
    private readonly List<IDataEntity> dataEntities;
    public override string InvokerName => ActionNames.CouchBase;

    public override string InvokerShortName => ActionNames.CouchBaseShort;

    private IDictionary<string, Func<string[], Task>> actions;

    public CouchDataBaseActionInvoker(IEnumerable<ICheckableRepository> checkableRepositories,
        IEnumerable<IDataEntity> dataEntities)
    {
        this.checkableRepositories = checkableRepositories.ToList();
        this.dataEntities = dataEntities.ToList();

        actions = new Dictionary<string, Func<string[], Task>>();
        actions.Add(ActionNames.CheckSubCommand, CheckAsync);
    }

    public async Task InvokeAsync(string[] args)
    {
        var command = args.Any() ? args[0] : string.Empty;
        var argsToPass = args.Any() ? args.Skip(1).ToArray() : Array.Empty<string>();

        if (actions.Any(x => x.Key == command))
        {
            await actions.FirstOrDefault(x => x.Key == command).Value(argsToPass);
        }
        else
        {
            Help(Array.Empty<string>());
        }
    }

    private async Task CheckAsync(string[] args)
    {
        if (args.Length == 0)
        {
            await CheckAndFixAllEntities();
        }
        else if (args[0] == "-f" && args.Length == 1)
        {
            await CheckAndFixAllEntities(true);
        }
    }

    // TODO: переписать механику вывода данных в консоль, возможно, использовать словарь или объект с указанием
    // названия столбца, шириной в табуляциях и самих перечислениях, вроде
    // IDictionary<string имя колонки, Tuple<int ширинав табуляциях, IEnumerable<string>> значения>
    private async Task CheckAndFixAllEntities(bool fixDataBase = false)
    {
        var dataEntitiesChecks = await dataEntities.ToAsyncEnumerable()
            .SelectAwait(async x => await CheckDataEntityInfrastructureAsync(x, fixDataBase)).ToListAsync();
        var output = dataEntitiesChecks.Select(
            x => List.Create(x.DataEntityName,
                    x.DiagnosticResult != DiagnosticResultsType.MissingRepository
                        ? true.ToString()
                        : DiagnosticResults.DiagnosticResultsMessages[x.DiagnosticResult],
                    x.DiagnosticResult == DiagnosticResultsType.Success
                        ? true.ToString()
                        : DiagnosticResults.DiagnosticResultsMessages[x.DiagnosticResult])
                .Concat(fixDataBase
                    ? List.Create(x.DataBaseFixSucceed?.ToString() ?? string.Empty)
                    : List.Create<string>()).ToList()).ToList();
        output = List.Create(List.Create("Entity name", "Repository implemented", "Database implemented")
                .Concat(fixDataBase ? List.Create("Database fixed") : List.Create<string>()).ToList())
            .Concat(output).ToList();

        var outputAsTable = ConsoleTableFormatter.FormatTableToConsoleOutput(output).ToList();

        Console.WriteLine();
        outputAsTable.ForEach(Console.WriteLine);
        Console.WriteLine();
    }

    private async Task<DataEntityCheckDetails> CheckDataEntityInfrastructureAsync(IDataEntity dataEntity,
        bool fixDataBase)
    {
        var repository = checkableRepositories.Find(r => r.GetDataModel.FullName == dataEntity.GetType().FullName);

        var checkResult = repository != null ? await repository.CheckState() : DiagnosticResultsType.MissingRepository;

        var fixDbSucceed = fixDataBase &&
                           checkResult != DiagnosticResultsType.Success &&
                           checkResult != DiagnosticResultsType.UnableToConnect &&
                           repository != default
            ? await repository.RestoreIndexAsync()
            : (bool?)null;

        return DataEntityCheckDetails.Create(dataEntity.GetType().Name, repository != null,
            repository != null && checkResult == DiagnosticResultsType.Success, checkResult, fixDbSucceed);
    }
}