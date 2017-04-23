using Microsoft.Win32.TaskScheduler;
using System;
using TaskScheduler.Core.Extensions;
using TaskScheduler.Core.Models.Base;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Shared.Enums;

namespace TaskScheduler.Core.Services
{
    public class TaskTriggerBuilder : ITaskTriggerBuilder
    {
        public Trigger BuildTrigger(BaseTaskModel task)
        {
            var trigger = TriggerFactory(task.StartDate, task.Recurrence);
            trigger.StartBoundary = task.StartDate;
            trigger.Enabled = true;

            if (trigger.GetType() == typeof(WeeklyTrigger))
            {
                trigger.EndBoundary = task.RecurrenceEndDate;
            }

            return trigger;
        }

        private Trigger TriggerFactory(DateTime triggerDate, RecurrenceType recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceType.Daily:
                    return new WeeklyTrigger
                    {
                        WeeksInterval = 1,
                        DaysOfWeek = DaysOfTheWeek.AllDays
                    };
                case RecurrenceType.Weekly:
                    return new WeeklyTrigger
                    {
                        WeeksInterval = 1,
                        DaysOfWeek = triggerDate.ToTaskDay()
                    };
                default:
                    // One off task
                    return new TimeTrigger();
            }
        }
    }
}
