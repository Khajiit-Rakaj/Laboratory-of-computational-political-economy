using System.Runtime.CompilerServices;
using MorseCode.ITask;

namespace UnitTests.BaseEntities;

internal class TaskAwaiterWrapper: IAwaiter
{
    private TaskAwaiter taskAwaiter;

    public TaskAwaiterWrapper(TaskAwaiter taskAwaiter)
    {
        this.taskAwaiter = taskAwaiter;
    }

    bool IAwaiter.IsCompleted
    {
        get
        {
            return this.taskAwaiter.IsCompleted;
        }
    }

    void INotifyCompletion.OnCompleted(Action continuation)
    {
        this.taskAwaiter.OnCompleted(continuation);
    }

    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
    {
        this.taskAwaiter.UnsafeOnCompleted(continuation);
    }

    void IAwaiter.GetResult()
    {
        this.taskAwaiter.GetResult();
    }
}

internal class TaskAwaiterWrapper<TResult> : IAwaiter<TResult>
{
    private TaskAwaiter<TResult> taskAwaiter;

    public TaskAwaiterWrapper(TaskAwaiter<TResult> taskAwaiter)
    {
        this.taskAwaiter = taskAwaiter;
    }

    bool IAwaiter<TResult>.IsCompleted
    {
        get
        {
            return this.taskAwaiter.IsCompleted;
        }
    }

    void INotifyCompletion.OnCompleted(Action continuation)
    {
        this.taskAwaiter.OnCompleted(continuation);
    }

    void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
    {
        this.taskAwaiter.UnsafeOnCompleted(continuation);
    }

    TResult IAwaiter<TResult>.GetResult()
    {
        return this.taskAwaiter.GetResult();
    }
}