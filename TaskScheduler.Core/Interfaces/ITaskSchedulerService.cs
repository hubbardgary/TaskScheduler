using System.Collections.Generic;
using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.Interfaces
{
    public interface ITaskSchedulerService<T, TI>
        where T : ITask<TI>
        where TI : ITaskModel
    {
        void AddTask(ITask<TI> customTask);
        void AddTask(BaseTask task);
        void DeleteTask(string id);
        void UpdateTask(T customTask);
        T GetTask(string taskName);
        IEnumerable<T> GetTasks();
    }
}
