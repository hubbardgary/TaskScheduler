using System;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Shared.Enums;

namespace TaskScheduler.Core.Models.Shutdown
{
    public class ShutdownModel : ITaskModel
    {
        public string Name { get; set; }
        public string PreviousName { get; set; }
        public DateTime ShutdownDateTime { get; set; }
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneOff;
        public DateTime RecurrenceEndDate { get; set; }
        public bool Enabled { get; set; }
    }
}
