using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TaskScheduler.Core.Enums;

namespace TaskScheduler.Web.Models.Shutdown
{
    public class ShutdownViewModel
    {
        [Required]
        [DisplayName("Name of task")]
        [Remote("ValidateUniqueShutdownName", "Shutdown", AdditionalFields = "PreviousName", ErrorMessage = "A shutdown task with that name already exists")]
        public string Name { get; set; }

        public string PreviousName { get; set; }

        [Required]
        [DisplayName("Date of shutdown")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShutdownDate { get; set; } = DateTime.Today;

        [Required]
        [DisplayName("Time of shutdown")]
        [DataType(DataType.Time)]
        public DateTime ShutdownTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 00);

        [DisplayName("Recurring Task")]
        public bool IsRecurring { get; set; }

        [DisplayName("Recurrence")]
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneOff;

        [DisplayName("Continue Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecurrenceEndDate { get; set; } = DateTime.Today.AddMonths(2);

        [DisplayName("Enabled")]
        public bool IsEnabled { get; set; } = true;

        public bool Selected { get; set; }
    }
}