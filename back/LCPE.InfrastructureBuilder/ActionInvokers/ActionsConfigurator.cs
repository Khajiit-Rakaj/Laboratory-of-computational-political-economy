namespace InfrastructureBuilder.ActionInvokers;

public class ActionsConfigurator : IActionConfigurator
{
    private IDictionary<string, (Func<string[], Task> action, Action<string[]> help)> actionCollection;

    private readonly IEnumerable<IActionInvoker> actionInvokers;

    public ActionsConfigurator(IEnumerable<IActionInvoker> actionInvokers)
    {
        this.actionInvokers = actionInvokers;
        SetupActions();
    }

    public IDictionary<string, (Func<string[], Task> action, Action<string[]> help)> GetConfiguration()
    {
        return actionCollection;
    }

    private void SetupActions()
    {
        actionCollection = new Dictionary<string, (Func<string[], Task> action, Action<string[]> help)>();

        actionCollection.Add(ActionNames.Help,
            (actionInvokers.First(x => x.GetType().Name == nameof(HelpActionInvoker)).InvokeAsync,
                actionInvokers.First(x => x.GetType().Name == nameof(HelpActionInvoker)).Help));
        actionCollection.Add(ActionNames.HelpShort,
            (actionInvokers.First(x => x.GetType().Name == nameof(HelpActionInvoker)).InvokeAsync,
                actionInvokers.First(x => x.GetType().Name == nameof(HelpActionInvoker)).Help));
    }
}