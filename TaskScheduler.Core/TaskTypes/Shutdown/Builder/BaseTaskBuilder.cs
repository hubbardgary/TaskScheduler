using System;
using TaskScheduler.Core.TaskTypes.Base;
using TaskScheduler.Shared.Enums;

namespace TaskScheduler.Core.TaskTypes.Shutdown.Builder
{
    public class BaseTaskBuilder
    {
        const string ExecAction = @"shutdown.exe";
        const string ExecActionArguments = @"-s -f -t 0";

        private BaseTask _task;

        public BaseTaskBuilder()
        {
            _task = new BaseTask();
            _task.Model.ExecAction = ExecAction;
            _task.Model.ExecActionArguments = ExecActionArguments;
        }

        public BaseTask Build()
        {
            return _task;
        }

        public BaseTaskBuilder SetName(string name)
        {
            _task.Model.Name = name;
            return this;
        }

        public BaseTaskBuilder SetStartDateTime(DateTime start)
        {
            _task.Model.StartDate = start;
            return this;
        }

        public BaseTaskBuilder SetEnabled(bool enabled)
        {
            _task.Model.IsEnabled = enabled;
            return this;
        }

        public BaseTaskBuilder SetRecurrence(RecurrenceType recurrence)
        {
            _task.Model.Recurrence = recurrence;
            _task.Model.IsRecurring = recurrence == RecurrenceType.OneOff ? false : true;
            return this;
        }

        public BaseTaskBuilder SetRecurrenceEndDate(DateTime date)
        {
            _task.Model.RecurrenceEndDate = date;
            return this;
        }
    }
}
