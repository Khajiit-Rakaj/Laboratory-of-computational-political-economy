using LCPE.Data.Interfaces.Repositories;
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
            await dataEntities.ToAsyncEnumerable().ForEachAwaitAsync(CheckDataEntityInfrastructureAsync);
        }
    }

    private async Task CheckDataEntityInfrastructureAsync(IDataEntity dataEntity)
    {
        Console.Write($"\n{dataEntity.GetType().Name}");
        var repository =
            checkableRepositories.FirstOrDefault(r => r.GetDataModel.FullName == dataEntity.GetType().FullName);
        
        if (repository != null)
        {
            var result = await repository.CheckState();
            Console.Write($"\t{result}");
        }
        else
        {
            Console.Write("\tMissing repository");
        }
    }
}