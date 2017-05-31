using System;
using TaskScheduler.Core.Enums;

namespace TaskScheduler.Web.Helpers
{
    public static class TaskUtils
    {
        public static string GetRecurrenceTooltipText(RecurrenceType recurrence, DateTime startDate, DateTime recurrenceEndDate)
        {
            return $"Task will run {GetRecurrenceTypeText(recurrence, startDate)} until {recurrenceEndDate.ToShortDateString()}";
        }

        private static string GetRecurrenceTypeText(RecurrenceType recurrence, DateTime startDate)
        {
            switch (recurrence)
            {
                case RecurrenceType.Daily:
                    return "daily";
                case RecurrenceType.Weekly:
                    return $"weekly every {startDate.DayOfWeek.ToString()}";
                case RecurrenceType.WeekDays:
                    return "every weekday";
                default:
                    return string.Empty;
            }
        }
    }
}