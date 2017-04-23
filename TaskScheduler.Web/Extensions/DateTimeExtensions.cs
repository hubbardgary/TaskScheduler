using System;

namespace TaskScheduler.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime SetTime(this DateTime date, DateTime time)
        {
            return new DateTime(
                date.Year,
                date.Month,
                date.Day,
                time.Hour,
                time.Minute,
                time.Second);
        }
    }
}