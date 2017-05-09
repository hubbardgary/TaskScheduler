using System;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Core.Enums;

namespace TaskScheduler.Core.TaskTypes.Shutdown.Builder
{
    public class ShutdownTaskBuilder
    {
        private ShutdownModel _task;

        public ShutdownTaskBuilder()
        {
            _task = new ShutdownModel();
        }

        public ShutdownTask Build()
        {
            return new ShutdownTask
            {
                Model = _task
            };
        }

        public ShutdownTaskBuilder SetName(string name)
        {
            _task.Name = name;
            return this;
        }

        public ShutdownTaskBuilder SetPreviousName(string previousName)
        {
            _task.PreviousName = previousName;
            return this;
        }

        public ShutdownTaskBuilder SetShutdownDateTime(DateTime date)
        {
            _task.ShutdownDateTime = date;
            return this;
        }

        public ShutdownTaskBuilder SetRecurrence(RecurrenceType recurrence, DateTime recurrenceEndDate)
        {
            _task.Recurrence = recurrence;

            if (recurrence == RecurrenceType.OneOff)
            {
                _task.RecurrenceEndDate = DateTime.Today.AddMonths(2);
            }
            else
            {
                _task.RecurrenceEndDate = recurrenceEndDate;    
            }
            return this;
        }

        public ShutdownTaskBuilder SetEnabled(bool enabled)
        {
            _task.Enabled = enabled;
            return this;
        }
    }
}
