namespace InfrastructureBuilder.ActionInvokers;

public abstract class ActionInvokerWithHelp
{
    public abstract string InvokerName { get; }
    public abstract string InvokerShortName { get; }
    
    protected virtual string Commands() => $"{InvokerName}\t{InvokerShortName}";
    
    protected virtual string Description() => $"Generic {GetType().Name} description.";
    
    protected virtual string ParametersList() => "[OPTIONS]";
    
    protected virtual string ParametersDescription() => "OPTIONS - commands";
    
    public virtual void Help(string[] args)
    {
        Console.WriteLine($"{Commands()}");
        Console.WriteLine($"\t{Description()}\n\tUsage:");
        Console.WriteLine($"\t{ParametersList()}");
        Console.WriteLine($"\t{ParametersDescription()}");
    }

}