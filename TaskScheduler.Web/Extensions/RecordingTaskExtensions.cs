using TaskScheduler.Core.TaskTypes.Recording;
using TaskScheduler.Web.Models.Recording;

namespace TaskScheduler.Web.Extensions
{
    public static class RecordingTaskExtensions
    {
        public static RecordingViewModel ToViewModel(this RecordingTask recording)
        {
            return new RecordingViewModel
            {
                Title = recording.Model.Title,
                ChannelID = recording.Model.Channel.Id,
                ChannelName = recording.Model.Channel.Name,
                StartDate = recording.Model.ProgrammeStart,
                StartTime = recording.Model.ProgrammeStart,
                EndDate = recording.Model.ProgrammeEnd,
                EndTime = recording.Model.ProgrammeEnd,
                IsRecurring = recording.Model.IsRecurring,
                Recurrence = recording.Model.Recurrence,
                RecurrenceEndDate = recording.Model.RecurrenceEndDate,
                IsEnabled = recording.Model.IsEnabled,
                PreviousTitle = recording.Model.PreviousTitle,
                Selected = false
            };
        }
    }
}