using LCPE.Extensions;

namespace InfrastructureBuilder.ActionInvokers;

public class HelpActionInvoker : ActionInvokerWithHelp, IHelpActionInvoker
{
    public override string InvokerName => ActionNames.Help;

    public override string InvokerShortName => ActionNames.HelpShort;

    private readonly IEnumerable<IActionInvoker> actionInvokers;

    public HelpActionInvoker(IEnumerable<IActionInvoker> actionInvokers)
    {
        this.actionInvokers = actionInvokers;
    }

    public async Task InvokeAsync(string[] args)
    {
        SelectHelpItem(args);
    }

    protected override string Description() => "Provide help.";

    protected override string ParametersDescription() => "OPTIONS - command to get help";

    public override void Help(string[] args)
    {
        base.Help(args);
        actionInvokers.ToList().ForEach(x => x.Help(args));
    }

    private void SelectHelpItem(string[] args)
    {
        var helpMethod = args.Any()
            ? actionInvokers.FirstOrDefault(x => x.InvokerName == args[0] || x.InvokerShortName == args[0])
            : null;

        if (helpMethod != null && helpMethod.InvokerName != ActionNames.Help)
        {
            helpMethod.Help(args);
        }
        else
        {
            Help(List.Create<string>().ToArray());
        }
    }
}