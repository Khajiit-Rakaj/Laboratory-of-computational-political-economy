using System.Runtime.CompilerServices;
using MorseCode.ITask;

namespace UnitTests.BaseEntities;

internal class ConfiguredTaskWrapper : IConfiguredTask
{
    private ConfiguredTaskAwaitable task;

    public ConfiguredTaskWrapper(Task task, bool continueOnCapturedContext)
    {
        this.task = task.ConfigureAwait(continueOnCapturedContext);
    }

    IAwaiter IConfiguredTask.GetAwaiter()
    {
        return new ConfiguredTaskAwaiterWrapper(this.task.GetAwaiter());
    }
}

internal class ConfiguredTaskWrapper<TResult> : IConfiguredTask<TResult>
{
    private ConfiguredTaskAwaitable<TResult> task;

    public ConfiguredTaskWrapper(Task<TResult> task, bool continueOnCapturedContext)
    {
        this.task = task.ConfigureAwait(continueOnCapturedContext);
    }

    IAwaiter<TResult> IConfiguredTask<TResult>.GetAwaiter()
    {
        return new ConfiguredTaskAwaiterWrapper<TResult>(this.task.GetAwaiter());
    }
}