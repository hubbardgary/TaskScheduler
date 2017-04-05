namespace TaskScheduler.Core.Interfaces
{
    public interface ITaskService<T, TI>
        where T : ITask<TI>
        where TI : ITaskModel
    {
    }
}
