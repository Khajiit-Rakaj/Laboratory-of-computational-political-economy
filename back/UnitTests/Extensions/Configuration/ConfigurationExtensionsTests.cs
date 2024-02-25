using UnitTests.Extensions.Configuration.Context;
using LCPE.Extensions;

namespace UnitTests.Extensions.Configuration;

[TestFixture]
public class ConfigurationExtensionsTests
{
    private ConfigurationExtensionsTestsContext context;

    [SetUp]
    public void SetUp()
    {
        context = new ConfigurationExtensionsTestsContext();
    }

    [Test]
    public void GetListValues_ConfigurationWithListOfStringsIsProvided_ListOfStringIsReturned()
    {
        context.SetupConfiguration();

        var result = context.Configuration.GetListValues<string>(ConfigurationExtensionsTestsContext.ConfigName);
        
        context
            .ResultEqualsToExpected(result)
            .Assert();
    }
}