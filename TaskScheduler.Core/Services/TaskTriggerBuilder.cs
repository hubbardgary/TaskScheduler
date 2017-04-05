using Microsoft.Win32.TaskScheduler;
using System;
using TaskScheduler.Core.Extensions;
using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Models.Base;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Services
{
    public class TaskTriggerBuilder : ITaskTriggerBuilder
    {
        private TaskService _taskService;

        public TaskTriggerBuilder(TaskService taskService)
        {
            _taskService = taskService;
        }

        public Trigger BuildTrigger(BaseTaskModel task)
        {
            Trigger trigger = TriggerFactory(task.StartDate, task.Recurrence);
            trigger.StartBoundary = task.StartDate;
            trigger.Enabled = true;

            return trigger;
        }

        private Trigger TriggerFactory(DateTime triggerDate, RecurrenceType recurrence)
        {
            if (recurrence == RecurrenceType.OneOff)
                return new TimeTrigger();

            return new WeeklyTrigger
            {
                WeeksInterval = 1,
                DaysOfWeek = recurrence == RecurrenceType.Daily ? DaysOfTheWeek.AllDays : triggerDate.ToTaskDay()
            };
        }
    }
}
