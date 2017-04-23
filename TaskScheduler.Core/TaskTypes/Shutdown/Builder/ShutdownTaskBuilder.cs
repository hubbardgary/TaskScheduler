using System;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Shared.Enums;

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

        public ShutdownTaskBuilder SetShutdownDateTime(DateTime date)
        {
            _task.ShutdownDateTime = date;
            return this;
        }

        public ShutdownTaskBuilder SetRecurrence(RecurrenceType recurrence)
        {
            _task.Recurrence = recurrence;
            return this;
        }
    }
}
