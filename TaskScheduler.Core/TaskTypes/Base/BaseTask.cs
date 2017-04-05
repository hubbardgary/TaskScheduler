using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Base;

namespace TaskScheduler.Core.TaskTypes.Base
{
    public class BaseTask : ITask<BaseTaskModel>
    {
        public BaseTaskModel Model { get; set; }

        public BaseTask(BaseTaskModel task)
        {
            Model = task;
        }

        public BaseTask()
        {
            Model = new BaseTaskModel();
        }

        public ITask<BaseTaskModel> ToCustomTaskModel(BaseTask winTask)
        {
            return this;
        }

        public BaseTask ToBaseTaskModel()
        {
            return this;
        }
    }
}
