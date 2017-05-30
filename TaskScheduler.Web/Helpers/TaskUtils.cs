using System;
using TaskScheduler.Core.Enums;

namespace TaskScheduler.Web.Helpers
{
    public static class TaskUtils
    {
        public static string GetRecurrenceTooltipText(RecurrenceType recurrence, DateTime startDate, DateTime recurrenceEndDate)
        {
            return $"Task will run {(recurrence == RecurrenceType.Daily ? "daily" : $"weekly every {startDate.DayOfWeek.ToString()}")} until {recurrenceEndDate.ToShortDateString()}";
        }
    }
}