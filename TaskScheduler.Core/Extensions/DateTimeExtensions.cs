using Microsoft.Win32.TaskScheduler;
using System;

namespace TaskScheduler.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DaysOfTheWeek ToTaskDay(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return DaysOfTheWeek.Monday;
                case DayOfWeek.Tuesday:
                    return DaysOfTheWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return DaysOfTheWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return DaysOfTheWeek.Thursday;
                case DayOfWeek.Friday:
                    return DaysOfTheWeek.Friday;
                case DayOfWeek.Saturday:
                    return DaysOfTheWeek.Saturday;
                case DayOfWeek.Sunday:
                    return DaysOfTheWeek.Sunday;
            }
            throw new Exception("ToTaskDay invoked with invalid date");
        }
    }
}
