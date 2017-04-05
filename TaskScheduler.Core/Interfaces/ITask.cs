using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.Interfaces
{
    public interface ITask<T> where T : ITaskModel
    {
        BaseTask ToBaseTaskModel();
        ITask<T> ToCustomTaskModel(BaseTask winTask);
    }
}
