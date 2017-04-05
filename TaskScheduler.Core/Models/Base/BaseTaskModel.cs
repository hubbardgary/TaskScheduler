using System;
using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Models.Base
{
    public class BaseTaskModel : ITaskModel
    {
        public string Name { get; set; }
        public string PreviousName { get; set; }
        public string Description { get; set; }
        public string TriggerTime { get; set; }
        public string ExecAction { get; set; }
        public string ExecActionArguments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextRunTime { get; set; }
        public DateTime LastRunTime { get; set; }
        public RecurrenceType Recurrence { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsExpired { get; set; }
        public bool IsEnabled { get; set; }
        public bool NameHasChanged => Name != PreviousName;
    }
}
