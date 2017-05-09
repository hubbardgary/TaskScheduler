using Microsoft.Win32.TaskScheduler;
using System;
using System.Linq;
using TaskScheduler.Core.Models.Base;
using TaskScheduler.Core.TaskTypes.Base;
using TaskScheduler.Core.Enums;

namespace TaskScheduler.Core.Extensions
{
    public static class Win32TaskExtensions
    {
        public static BaseTask ToWindowsTask(this Task task)
        {
            if (task == null)
                return null;

            var trigger = task.Definition.Triggers.First();

            var job = new BaseTaskModel
            {
                Name = task.Name,
                PreviousName = task.Name,
                Description = task.Definition.RegistrationInfo.Description,
                TriggerTime = trigger.ToString(),
                StartDate = trigger.StartBoundary,
                NextRunTime = task.NextRunTime,
                LastRunTime = task.LastRunTime,
                ExecAction = task.Definition.Actions.First().ToString(),
                ExecActionArguments = task.Definition.Actions.First().ToString(),
                Recurrence = GetRecurrence(task),
                IsRecurring = trigger.ToString().Contains("every"),
                RecurrenceEndDate = task.Definition.Triggers.First().EndBoundary,
                IsExpired = HasExpired(trigger.ToString()),
                IsEnabled = task.Enabled
            };

            return new BaseTask(job);
        }

        private static bool HasExpired(string trigger)
        {
            if (trigger.Contains("expire"))
            {
                var expDate = Convert.ToDateTime(trigger.Substring(trigger.IndexOf("expire", StringComparison.CurrentCulture) + 11, 16));
                if (expDate < DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        private static RecurrenceType GetRecurrence(Task task)
        {
            var trigger = task.Definition.Triggers.First();
            if (trigger is TimeTrigger)
            {
                return RecurrenceType.OneOff;
            }

            var weeklyTrigger = trigger as WeeklyTrigger;
            if (weeklyTrigger != null)
            {
                if (weeklyTrigger.DaysOfWeek == DaysOfTheWeek.AllDays)
                {
                    return RecurrenceType.Daily;
                }

                return RecurrenceType.Weekly;
            }
            throw new Exception("Unable to determine task recurrence.");
        }
    }
}
