namespace InfrastructureBuilder.ActionInvokers;

public interface IInvoker
{
    string InvokerName { get; }
    
    string InvokerShortName { get; }

    Task InvokeAsync(string[] args);

    void Help(string[] args);
}