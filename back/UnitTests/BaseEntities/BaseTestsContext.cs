using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using FizzWare.NBuilder;
using Moq;
using MorseCode.ITask;
using Shouldly;

namespace UnitTests.BaseEntities;

public abstract class BaseTestsContext
{
    protected AssertionGroup Assertions = new AssertionGroup();

    protected BaseTestsContext()
    {
        Mocker = AutoMock.GetLoose();
    }

    public AutoMock Mocker { get; protected set; }

    public void Assert()
    {
        Assertions.Assert();
    }

    protected Mock<TService> MockService<TService, TResult>(
        Expression<Func<TService, TResult>> expression,
        TResult result = default,
        int executionCount = 1)
        where TService : class
    {
        var mock = Mocker.Mock<TService>();
        mock.Setup(expression)
            .Returns(EqualityComparer<TResult>.Default.Equals(result, default)
                ? Builder<TResult>.CreateNew().Build()
                : result);

        Assertions.Append(() =>
            Mocker.Mock<TService>().Verify(expression, Times.Exactly(executionCount)));

        return mock;
    }

    protected Mock<TService> MockService<TService, TResult>(
        Expression<Func<TService, Task<TResult>>> expression,
        TResult result = default,
        int executionCount = 1)
        where TService : class
    {
        var mock = Mocker.Mock<TService>();
        mock.Setup(expression)
            .Returns(Task.FromResult(EqualityComparer<TResult>.Default.Equals(result, default)
                ? Builder<TResult>.CreateNew().Build()
                : result));

        Assertions.Append(() =>
            Mocker.Mock<TService>().Verify(expression, Times.Exactly(executionCount)));

        return mock;
    }

    protected Mock<TService> MockService<TService, TResult>(
        Expression<Func<TService, ITask<TResult>>> expression,
        TResult result = default,
        int executionCount = 1)
        where TService : class
    {
        var mock = Mocker.Mock<TService>();
        mock.Setup(expression)
            .Returns(new TaskWrapper<TResult>(Task.FromResult(EqualityComparer<TResult>.Default.Equals(result, default)
                ? Builder<TResult>.CreateNew().Build()
                : result)));

        Assertions.Append(() =>
            Mocker.Mock<TService>().Verify(expression, Times.Exactly(executionCount)));

        return mock;
    }

    protected Mock<TService> MockService<TService>(
        Expression<Action<TService>> expression,
        int executionCount = 1)
        where TService : class
    {
        return MockService(expression, Times.Exactly(executionCount));
    }

    private Mock<TService> MockService<TService>(Expression<Action<TService>> expression, Times times)
        where TService : class
    {
        var mock = Mocker.Mock<TService>();
        mock.Setup(expression);

        Assertions.Append(() => Mocker.Mock<TService>().Verify(expression, times));

        return mock;
    }

    protected BaseTestsContext VerifyServiceNeverCalled<TService>(Expression<Action<TService>> expression)
        where TService : class
    {
        Mocker.Mock<TService>().Verify(expression, Times.Never);

        return this;
    }

    public BaseTestsContext ServicesShouldBeExecuted()
    {
        Assertions.Append(Mocker.MockRepository.Verify);

        return this;
    }

    public BaseTestsContext ResultShouldBeReturned<T>(T result)
        where T : class
    {
        Assertions.Append(() => result.ShouldNotBeNull());

        return this;
    }

    public BaseTestsContext ResultShouldBeNull<T>(T result)
        where T : class
    {
        Assertions.Append(() => result.ShouldBeNull());

        return this;
    }

    public BaseTestsContext ResultShouldBeTrue(bool result)
    {
        Assertions.Append(() => result.ShouldBeTrue());

        return this;
    }

    public BaseTestsContext ResultShouldBeFalse(bool result)
    {
        Assertions.Append(() => result.ShouldBeFalse());

        return this;
    }

    public BaseTestsContext ResultCollectionShouldBeEmpty<T>(IEnumerable<T> result)
        where T : class
    {
        Assertions.Append(() => result.ShouldBeEmpty());

        return this;
    }

    public BaseTestsContext ResultCollectionShouldNotBeEmpty<T>(IEnumerable<T> result)
        where T : class
    {
        Assertions.Append(() => result.ShouldNotBeEmpty());

        return this;
    }
}