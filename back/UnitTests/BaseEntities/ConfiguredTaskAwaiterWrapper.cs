using System.Runtime.CompilerServices;
using MorseCode.ITask;

namespace UnitTests.BaseEntities;

internal class ConfiguredTaskAwaiterWrapper<TResult> : IAwaiter<TResult>
{
    private ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter taskAwaiter;

    public ConfiguredTaskAwaiterWrapper(ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter taskAwaiter)
    {
        this.taskAwaiter = taskAwaiter;
    }

    bool IAwaiter<TResult>.IsCompleted
    {
        get
        {
            return taskAwaiter.IsCompleted;
        }
    }

    void INotifyCompletion.OnCompleted(Action continuation)
    {
        taskAwaiter.OnCompleted(continuation);
    }

    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
    {
        taskAwaiter.UnsafeOnCompleted(continuation);
    }

    TResult IAwaiter<TResult>.GetResult()
    {
        return taskAwaiter.GetResult();
    }
}

internal class ConfiguredTaskAwaiterWrapper : IAwaiter
{
    private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter;

    public ConfiguredTaskAwaiterWrapper(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter)
    {
        this.taskAwaiter = taskAwaiter;
    }

    bool IAwaiter.IsCompleted
    {
        get
        {
            return taskAwaiter.IsCompleted;
        }
    }

    void INotifyCompletion.OnCompleted(Action continuation)
    {
        taskAwaiter.OnCompleted(continuation);
    }

    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
    {
        taskAwaiter.UnsafeOnCompleted(continuation);
    }

    void IAwaiter.GetResult()
    {
        taskAwaiter.GetResult();
    }
}