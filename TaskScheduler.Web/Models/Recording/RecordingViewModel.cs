using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Shared.Enums;

namespace TaskScheduler.Web.Models.Recording
{
    public class RecordingViewModel
    {
        public string id { get; set; }

        [DisplayName("Show Title")]
        [Required]
        [Remote("ValidateUniqueShowTitle", "Recording", AdditionalFields = "PreviousTitle", ErrorMessage = "A show with that title already exists")]
        public string Title { get; set; }

        [DisplayName("Channel ID")]
        [Required]
        public int ChannelID { get; set; }

        [DisplayName("Channel")]
        public string ChannelName { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  // HTML5 compliant browsers need date to be in this format.
        [Required]
        public DateTime StartDate { get; set; }

        [DisplayName("Start Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime StartTime { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndDate { get; set; }

        [DisplayName("End Time")]
        [DataType(DataType.Time)]
        [Required]
        public DateTime EndTime { get; set; }

        [DisplayName("Recurring Task")]
        public bool IsRecurring { get; set; }

        [DisplayName("Recurrance")]
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneOff;

        [DisplayName("Continue Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecurrenceEndDate { get; set; } = DateTime.Today.AddMonths(2);

        [DisplayName("Enabled")]
        public bool IsEnabled { get; set; } = true;

        // Only used for validating Edit model
        public string PreviousTitle { get; set; } = "";

        public bool Selected { get; set; }

        public IEnumerable<ChannelModel> Channels;

        [DisplayName("Create Shutdown Task")]
        public bool CreateShutdownTask { get; set; }

        public RecordingViewModel()
        {
            Channels = Core.Helpers.ChannelLookup.AllChannels.Select(c => new ChannelModel { Id = c.Key, Name = c.Value }).ToList();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            StartTime = DateTime.Now.AddMinutes(10);
            EndTime = StartTime.AddMinutes(30);
            Selected = false;
        }
    }
}