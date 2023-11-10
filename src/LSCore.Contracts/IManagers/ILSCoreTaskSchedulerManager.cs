using LSCore.Contracts.Tasks;

namespace LSCore.Contracts.IManagers
{
    public interface ILSCoreTaskSchedulerManager
    {
        LSCoreTaskSchedulerState State { get; }
        Task RunTasksAsync(bool runTasksAsync);
        void AddTask(LSCoreTask task);
        void Stop();
    }
}
