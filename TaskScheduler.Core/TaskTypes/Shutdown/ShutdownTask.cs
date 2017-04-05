using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Core.TaskTypes.Base;
using TaskScheduler.Core.TaskTypes.Shutdown.Builder;

namespace TaskScheduler.Core.TaskTypes.Shutdown
{
    public class ShutdownTask : ITask<ShutdownModel>
    {
        public ShutdownModel Model { get; set; }

        public ITask<ShutdownModel> ToCustomTaskModel(BaseTask task)
        {
            var builder = new ShutdownTaskBuilder();
            return builder
                .SetName(task.Model.Name)
                .SetShutdownDateTime(task.Model.StartDate)
                .SetRecurrence(task.Model.Recurrence)
                .Build();
        }

        public BaseTask ToBaseTaskModel()
        {
            var builder = new BaseTaskBuilder();
            return builder
                .SetName(Model.Name)
                .SetStartDateTime(Model.ShutdownDateTime)
                .SetRecurrence(Model.Recurrence)
                .Build();
        }
    }
}
