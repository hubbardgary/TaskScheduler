using Microsoft.Win32.TaskScheduler;
using System;
using System.Linq;
using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Models.Base;
using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.Extensions
{
    public static class Win32TaskExtensions
    {
        public static BaseTask ToWindowsTask(this Task task)
        {
            if (task == null)
                return null;

            var trigger = task.Definition.Triggers.First().ToString().ToLower();

            var job = new BaseTaskModel
            {
                Name = task.Name,
                PreviousName = task.Name,
                Description = task.Definition.RegistrationInfo.Description,
                TriggerTime = task.Definition.Triggers.First().ToString(),
                StartDate = task.Definition.Triggers.First().StartBoundary,
                NextRunTime = task.NextRunTime,
                LastRunTime = task.LastRunTime,
                ExecAction = task.Definition.Actions.First().ToString(),
                ExecActionArguments = task.Definition.Actions.First().ToString(),
                Recurrence = GetRecurrence(task),
                IsRecurring = trigger.Contains("every"),
                IsExpired = HasExpired(trigger),
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
            if (trigger.GetType() == typeof(TimeTrigger))
            {
                return RecurrenceType.OneOff;
            }
            else if (trigger.GetType() == typeof(WeeklyTrigger))
            {
                var weeklyTrigger = (WeeklyTrigger)trigger;
                if (weeklyTrigger.DaysOfWeek == DaysOfTheWeek.AllDays)
                {
                    return RecurrenceType.Daily;
                }
                else
                {
                    return RecurrenceType.Weekly;
                }
            }
            throw new Exception("Unable to determine task recurrence.");
        }
    }
}
