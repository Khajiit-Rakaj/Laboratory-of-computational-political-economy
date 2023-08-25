using LCPE.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using UnitTests.BaseEntities;
using List = LCPE.Extensions.List;

namespace UnitTests.Extensions.Configuration.Context;

public class ConfigurationExtensionsTestsContext : BaseTestsContext
{
    public IConfiguration Configuration;
    public const string ConfigName = "ConfigName";
    private IEnumerable<string> ConfigValue = List.Create("\"A\"", "\"B\"", "\"C\"");

    public ConfigurationExtensionsTestsContext SetupConfiguration()
    {
        var childConfig = Mocker.Mock<ConfigurationSection>();
        childConfig.Setup(x => x.Value).Returns($"[{string.Join(",", ConfigValue)}]");

        var config = Mocker.Mock<IConfiguration>();
        config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(childConfig.Object);

        Configuration = config.Object;

        return this;
    }
}