using System;
using System.Collections.Generic;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Shared.Enums;

namespace TaskScheduler.Core.Models.Recording
{
    public class RecordingModel : ITaskModel
    {
        public string Id { get; set; }
        public static int OffsetMinutes { get; set; } = 5;
        public string Title { get; set; }
        public ChannelModel Channel { get; set; }
        public DateTime ProgrammeStart { get; set; }
        public DateTime ProgrammeEnd { get; set; }
        public DateTime RecordingStart { get; set; }
        public DateTime RecordingEnd { get; set; }
        public bool IsRecurring { get; set; }
        public RecurrenceType Recurrence { get; set; } = RecurrenceType.OneOff;
        public DateTime RecurrenceEndDate { get; set; }
        public bool IsEnabled { get; set; }
        public string PreviousTitle { get; set; } = "";
        public bool Selected { get; set; }
        public IEnumerable<ChannelModel> Channels;

        public int ProgrammeDurationMinutes => (int)(ProgrammeEnd - ProgrammeStart).TotalMinutes;
        public int RecordingDurationMinutes => (int)(RecordingEnd - RecordingStart).TotalMinutes;
    }
}
