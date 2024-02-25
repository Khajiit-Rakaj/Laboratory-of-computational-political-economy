namespace InfrastructureBuilder.ActionInvokers;

public interface IActionConfigurator
{
    IDictionary<string, (Func<string[], Task> action, Action<string[]> help)> GetConfiguration();
}