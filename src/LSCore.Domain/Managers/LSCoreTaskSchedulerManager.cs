using LSCore.Contracts.Tasks;

namespace LSCore.Domain.Managers
{
    public class LSCoreTaskSchedulerManager
    {
        private List<Task> _asyncTasks { get; set; } = new List<Task>();
        private List<LSCoreTask> _tasks { get; set; } = new List<LSCoreTask>();
        private LSCoreTaskSchedulerState _state { get; set; } = LSCoreTaskSchedulerState.Idle;
        private CancellationTokenSource _cancellationTokenSource { get; set; }
        private CancellationToken _cancellationToken { get; set; }
        public LSCoreTaskSchedulerState State => _state;

        /// <summary>
        /// Adds task to the task scheduler.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(LSCoreTask task)
        {
            _tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runTasksAsync">If true tasks will run asynchonously each on separate thread with timeout between running.
        /// If false tasks will run synchonously one after another, waiting for pevious task to finish.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public System.Threading.Tasks.Task RunTasksAsync(bool runTasksAsync)
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                if (_state == LSCoreTaskSchedulerState.Running)
                    throw new Exception("Task scheduler is already running! Stop it before starting it again!");

                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                _state = LSCoreTaskSchedulerState.Running;

                if (runTasksAsync)
                    StartRunningTasksAsAsync();
                else
                    StartRunningTasksAsSync();
            });
        }

        private void StartRunningTasksAsAsync()
        {
            foreach (var task in _tasks)
            {
                _asyncTasks.Add(System.Threading.Tasks.Task.Run(() =>
                {
                    while (!_cancellationToken.IsCancellationRequested)
                    {
                        System.Threading.Tasks.Task.Run(task.Action, _cancellationToken).Wait();
                        if (_cancellationToken.IsCancellationRequested)
                            return;

                        Timeout(task.Timeout, _cancellationToken);
                    }
                }));
            }

            _ = WatchAsyncTasksAsync();
        }

        private void StartRunningTasksAsSync()
        {
            while (!_cancellationToken.IsCancellationRequested)
                foreach (var task in _tasks)
                    task.Action();
        }

        private System.Threading.Tasks.Task WatchAsyncTasksAsync()
        {
            return System.Threading.Tasks.Task.Run(() =>
            {
                bool clearStatus = true;

                foreach (var asyncTask in _asyncTasks)
                    if (asyncTask.Status == TaskStatus.Running)
                        clearStatus = false;

                if (clearStatus)
                    _state = LSCoreTaskSchedulerState.Idle;

                Thread.Sleep(TimeSpan.FromSeconds(1));
            });
        }

        private void Timeout(TimeSpan timeout, CancellationToken cancelationToken)
        {
            for (int i = 0; i < timeout.TotalSeconds; i++)
            {
                Thread.Sleep(1000);
                if (cancelationToken.IsCancellationRequested)
                    return;
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
