using InfrastructureBuilder.ActionInvokers;
using LCPE.Extensions;

namespace InfrastructureBuilder;

public class ConsoleProcessor : IConsoleProcessor
{
    private readonly IEnumerable<IActionInvoker> actionInvokers;
    private readonly IHelpActionInvoker helpActionInvoker;

    public ConsoleProcessor(IEnumerable<IActionInvoker> actionInvokers, IHelpActionInvoker helpActionInvoker)
    {
        this.actionInvokers = actionInvokers;
        this.helpActionInvoker = helpActionInvoker;
    }

    public async Task ContinuousProcessingAsync()
    {
        Console.Clear();
        do
        {
            Console.WriteLine("Input command or -h for help:");
            Console.Write("#>");
            var command = Console.ReadLine() ?? string.Empty;

            if (command == ActionNames.Exit)
            {
                Console.WriteLine("See you later!");
                break;
            }

            await ProcessAsync(command);
        } while (true);
    }

    private async Task ProcessAsync(string command)
    {
        var args = command.Split(" ").Where(x => x.IsNotNullOrWhiteSpace()).ToList();

        var invoker = args.Any() ? actionInvokers.FirstOrDefault(x => x.InvokerName == args[0] || x.InvokerShortName == args[0]) : null;
        var argsToPass = args.Any() ? args.Skip(1).ToArray() : Array.Empty<string>();

        if (invoker != null)
        {
            await invoker.InvokeAsync(argsToPass);
        }
        else
        {
            await helpActionInvoker.InvokeAsync(argsToPass);
        }
    }
}