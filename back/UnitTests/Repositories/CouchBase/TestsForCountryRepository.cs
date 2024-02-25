using Autofac;
using LCPE.Data.Repository;
using UnitTests.BaseEntities;
using UnitTests.Repositories.CouchBase.Context;

namespace UnitTests.Repositories.CouchBase;

[TestFixture]
public class TestsForCountryRepository : BaseTest<TestsForCountryRepositoryContext, CountryRepository>
{
    [SetUp]
    public void SetUp()
    {
        context = new TestsForCountryRepositoryContext();

        var factoryMock = context.MockClientFactory();
        var config = context.MockCouchBaseConfiguration();
        context.MockLog();

        subject = context.Mocker.Create<CountryRepository>(new PositionalParameter(0, factoryMock),
            new PositionalParameter(1, config));
    }

    [Test]
    public async Task SearchAsync_EmptyQueryIsProvided_DefaultSearchQueryIsSent()
    {
        context.SetupEmptyQuery().SetupClientSearch("select * from table_place_holder limit 10 offset 10");

        await subject.SearchAsync(context.QueryBuilder);

        context.Assert();
    }

    [Test]
    public async Task SearchAsync_QueryWithReturnFieldIsProvided_DefaultSearchQueryIsSent()
    {
        context.SetupQueryWithReturnFields()
            .SetupClientSearch("select Name,ShortName,Capital,Id from table_place_holder limit 10 offset 10");

        await subject.SearchAsync(context.QueryBuilder);

        context.Assert();
    }

    [Test]
    public async Task SearchAsync_QueryWithSearchFieldIsProvided_DefaultSearchQueryIsSent()
    {
        context.SetupQueryWithSearchFields().SetupClientSearch(
            "select * from table_place_holder where Name in (\"S1\",\"S2\") And ShortName in (\"s1\",\"s2\") And Id in (\"1\",\"2\") limit 10 offset 10");

        await subject.SearchAsync(context.QueryBuilder);

        context.Assert();
    }

    [Test]
    public async Task SearchAsync_QueryWithPagingIsProvided_DefaultSearchQueryIsSent()
    {
        context.SetupQueryWithPaging().SetupClientSearch(
            "select * from table_place_holder limit 100 offset 300");

        await subject.SearchAsync(context.QueryBuilder);

        context.Assert();
    }

    [Test]
    public async Task SearchAsync_QueryWithOrderingIsProvided_DefaultSearchQueryIsSent()
    {
        context.SetupQueryWithOrdering().SetupClientSearch(
            "select * from table_place_holder order by Name Desc,ShortName Asc limit 10 offset 10");

        await subject.SearchAsync(context.QueryBuilder);

        context.Assert();
    }
}