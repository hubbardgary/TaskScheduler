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
                .SetEnabled(task.Model.IsEnabled)
                .SetRecurrence(task.Model.Recurrence)
                .SetRecurrenceEndDate(task.Model.RecurrenceEndDate)
                .Build();
        }

        public BaseTask ToBaseTaskModel()
        {
            var builder = new BaseTaskBuilder();
            return builder
                .SetName(Model.Name)
                .SetPreviousName(Model.PreviousName)
                .SetStartDateTime(Model.ShutdownDateTime)
                .SetEnabled(Model.Enabled)
                .SetRecurrence(Model.Recurrence)
                .SetRecurrenceEndDate(Model.RecurrenceEndDate)
                .Build();
        }
    }
}
