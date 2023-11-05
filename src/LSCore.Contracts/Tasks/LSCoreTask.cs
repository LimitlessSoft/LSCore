namespace LSCore.Contracts.Tasks
{
    public class LSCoreTask
    {
        public TimeSpan Timeout { get; set; }
        public Action Action { get; set; }

        public LSCoreTask(Action action)
        {
            Action = action;
            Timeout = TimeSpan.Zero;
        }

        public LSCoreTask(Action action, TimeSpan timeout)
        {
            Timeout = timeout;
            Action = action;
        }
    }
}
