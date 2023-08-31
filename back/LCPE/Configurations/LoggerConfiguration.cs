using log4net.Config;

namespace LCPE.Configurations;

public class LoggerConfiguration
{
    public static void Configure()
    {
        XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
    }
}