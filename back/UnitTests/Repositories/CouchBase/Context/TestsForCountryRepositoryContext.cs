using FizzWare.NBuilder;
using LCPE.Configurations;
using LCPE.Data.BaseDataEntities;
using LCPE.Data.Interfaces;
using LCPE.Data.Queries;
using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;
using log4net;
using Moq;
using UnitTests.BaseEntities;
using List = LCPE.Extensions.List;

namespace UnitTests.Repositories.CouchBase.Context;

public class TestsForCountryRepositoryContext : BaseTestsContext
{
    public IQueryBuilder<CountryQuery> QueryBuilder { get; set; }

    public TestsForCountryRepositoryContext SetupEmptyQuery()
    {
        var queryBuilderMock = Mocker.Mock<IQueryBuilder<CountryQuery>>();
        var query = Builder<CountryQuery>.CreateNew()
            .Build();
        queryBuilderMock.Setup(x => x.Query).Returns(query);
        
        QueryBuilder = queryBuilderMock.Object;

        return this;
    }

    public TestsForCountryRepositoryContext SetupClientSearch(string query = "")
    {
        MockService<ICouchBaseClient<Country>, Task<IEnumerable<Country>>>(
            x => x.SearchAsync(It.Is<object>(q => q.ToString() == query)),
            Task.FromResult(List.Create<Country>() as IEnumerable<Country>));

        MockService<ICouchBaseClient<Country>, Task<IEnumerable<Country>>>(
            x => x.SearchAsync(It.IsAny<object>()), Task.FromResult(List.Create<Country>() as IEnumerable<Country>));

        MockService<ICouchBaseClient<Country>, string>(
            x => x.TablePlaceHolder, "table_place_holder");

        return this;
    }

    public ICouchBaseClientFactory<Country> MockClientFactory()
    {
        var client = Mocker.Mock<ICouchBaseClient<Country>>();
        var factory = Mocker.Mock<ICouchBaseClientFactory<Country>>();
        factory.Setup(x =>
                x.CreateAsync(It.IsAny<ConnectionConfiguration>(), It.IsAny<IndexConfiguration>(), It.IsAny<ILog>()))
            .Returns(Task.FromResult(client.Object));

        return factory.Object;
    }

    public CouchBaseConfiguration MockCouchBaseConfiguration()
    {
        return Builder<CouchBaseConfiguration>.CreateNew()
            .With(x => x.CouchBaseOptions = Builder<CouchBaseOptions>.CreateNew().Build()).Build();
    }

    public TestsForCountryRepositoryContext MockLog()
    {
        Mocker.Mock<ILog>();

        return this;
    }

    public TestsForCountryRepositoryContext SetupQueryWithReturnFields()
    {
        var queryBuilderMock = Mocker.Mock<IQueryBuilder<CountryQuery>>();
        var query = Builder<CountryQuery>.CreateNew()
            .With(q => q.ReturnFields = Builder<CountryReturnFields>.CreateNew()
                .With(r => r.Capital = true)
                .With(r => r.Name = true)
                .With(r => r.ShortName = true)
                .With(r => r.Id = true)
                .Build())
            .Build();
        queryBuilderMock.Setup(x => x.Query).Returns(query);
        
        QueryBuilder = queryBuilderMock.Object;

        return this;
    }

    public TestsForCountryRepositoryContext SetupQueryWithSearchFields()
    {
        var queryBuilderMock = Mocker.Mock<IQueryBuilder<CountryQuery>>();
        var query = Builder<CountryQuery>.CreateNew()
            .With(q => q.SearchFields = Builder<CountrySearchFields>.CreateNew()
                .With(r => r.ShortNames = List.Create("s1", "s2"))
                .With(r => r.Names = List.Create("S1", "S2"))
                .With(r => r.Ids = List.Create("1", "2"))
                .Build())
            .Build();
        queryBuilderMock.Setup(x => x.Query).Returns(query);
        
        QueryBuilder = queryBuilderMock.Object;

        return this;
    }

    public TestsForCountryRepositoryContext SetupQueryWithPaging()
    {
        var queryBuilderMock = Mocker.Mock<IQueryBuilder<CountryQuery>>();
        var query = Builder<CountryQuery>.CreateNew()
            .With(q => q.PageSize = 100)
            .With(q => q.FromPage = 3)
            .Build();
        queryBuilderMock.Setup(x => x.Query).Returns(query);
        
        QueryBuilder = queryBuilderMock.Object;
        
        return this;
    }

    public TestsForCountryRepositoryContext SetupQueryWithOrdering()
    {
        var queryBuilderMock = Mocker.Mock<IQueryBuilder<CountryQuery>>();
        var query = Builder<CountryQuery>.CreateNew()
            .With(q => q.SortingFields = Builder<CountrySortingFields>.CreateNew()
                .With(r => r.ShortName = SortingTypes.Asc)
                .With(r => r.Name = SortingTypes.Desc)
                .Build())
            .Build();
        queryBuilderMock.Setup(x => x.Query).Returns(query);
        
        QueryBuilder = queryBuilderMock.Object;
        
        return this;
    }
}