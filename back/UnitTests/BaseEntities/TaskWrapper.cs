using MorseCode.ITask;
using System.Threading.Tasks;

namespace UnitTests.BaseEntities;

internal class TaskWrapper<TResult> : ITask<TResult>
{
    private readonly Task<TResult> task;

    public TaskWrapper(Task<TResult> task)
    {
        this.task = task;
    }

    TResult ITask<TResult>.Result
    {
        get
        {
            return this.task.Result;
        }
    }

    IAwaiter ITask.GetAwaiter()
    {
        return new TaskAwaiterWrapper(((Task) this.task).GetAwaiter());
    }

    IAwaiter<TResult> ITask<TResult>.GetAwaiter()
    {
        return new TaskAwaiterWrapper<TResult>(this.task.GetAwaiter());
    }

    IConfiguredTask ITask.ConfigureAwait(bool continueOnCapturedContext)
    {
        return new ConfiguredTaskWrapper(this.task, continueOnCapturedContext);
    }

    IConfiguredTask<TResult> ITask<TResult>.ConfigureAwait(bool continueOnCapturedContext)
    {
        return new ConfiguredTaskWrapper<TResult>(this.task, continueOnCapturedContext);
    }
}