using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Base;

namespace TaskScheduler.Core.Services
{
    public class TaskTriggerBuilder : ITaskTriggerBuilder
    {
        public Trigger BuildTrigger(BaseTaskModel task)
        {
            var trigger = TriggerFactory.GetTrigger(task.StartDate, task.Recurrence);
            trigger.StartBoundary = task.StartDate;
            trigger.Enabled = true;

            if (trigger.GetType() == typeof(WeeklyTrigger))
            {
                // End boundary is inclusive so should expire at the end of the day.
                trigger.EndBoundary = task.RecurrenceEndDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            return trigger;
        }
    }
}
