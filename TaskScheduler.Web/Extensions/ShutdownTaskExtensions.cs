using TaskScheduler.Core.TaskTypes.Shutdown;
using TaskScheduler.Core.Enums;
using TaskScheduler.Web.Models.Shutdown;

namespace TaskScheduler.Web.Extensions
{
    public static class ShutdownTaskExtensions
    {
        public static ShutdownViewModel ToViewModel(this ShutdownTask shutdown)
        {
            return new ShutdownViewModel
            {
                Name = shutdown.Model.Name,
                PreviousName = shutdown.Model.Name,
                ShutdownDate = shutdown.Model.ShutdownDateTime,
                ShutdownTime = shutdown.Model.ShutdownDateTime,
                IsEnabled = shutdown.Model.Enabled, 
                IsRecurring = shutdown.Model.Recurrence != RecurrenceType.OneOff,
                Recurrence = shutdown.Model.Recurrence,
                RecurrenceEndDate = shutdown.Model.RecurrenceEndDate
            };
        }
    }
}