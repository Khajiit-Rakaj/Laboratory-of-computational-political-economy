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

    [Ignore("requires mock tuneup")]
    [Test]
    public void m_i_r()
    {
        context.SetupConfiguration();

        var result = context.Configuration.GetListValues<string>(ConfigurationExtensionsTestsContext.ConfigName);
    }
}