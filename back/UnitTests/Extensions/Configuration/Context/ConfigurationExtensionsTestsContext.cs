using FizzWare.NBuilder;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;
using UnitTests.BaseEntities;
using List = LCPE.Extensions.List;

namespace UnitTests.Extensions.Configuration.Context;

public class ConfigurationExtensionsTestsContext : BaseTestsContext
{
    public IConfiguration Configuration;
    public const string ConfigName = "ConfigName";
    private IEnumerable<string> ConfigValue = List.Create("A", "B", "C");

    public ConfigurationExtensionsTestsContext SetupConfiguration()
    {
        var childValues = ConfigValue.Select(value =>
            {
                var sectionMock = new Mock<IConfigurationSection>();
                sectionMock.Setup(x => x.Value).Returns(value);

                return sectionMock.Object;
            }
        ).ToList();

        var childConfig = Mocker.Mock<IConfigurationSection>();

        childConfig.Setup(x => x.GetChildren()).Returns(childValues);

        var config = Mocker.Mock<IConfiguration>();
        config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(childConfig.Object);

        Configuration = config.Object;

        return this;
    }

    public ConfigurationExtensionsTestsContext ResultEqualsToExpected(List<string> result)
    {
        Assertions.AppendMultiple(
            () => result.ShouldAllBe(x => ConfigValue.Contains(x) && result.Count(y => y == x) == 1),
            () => result.Count.ShouldBe(ConfigValue.Count())
            );

        return this;
    }
}