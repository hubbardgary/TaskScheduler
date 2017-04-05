using System;
using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Models.Shutdown
{
    public class ShutdownModel : ITaskModel
    {
        public string Name { get; set; }
        public DateTime ShutdownDateTime { get; set; }
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneOff;
    }
}
