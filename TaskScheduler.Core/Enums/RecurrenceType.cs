using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.Core.Enums
{
    public enum RecurrenceType
    {
        [Display(Name = "One off")]
        OneOff = 1,

        [Display(Name = "Daily")]
        Daily = 2,

        [Display(Name = "Weekly")]
        Weekly = 3,

        [Display(Name = "Week days")]
        WeekDays = 4
    }
}
