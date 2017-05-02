using System.Collections.Generic;
using System.Linq;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.TaskTypes.Recording;
using TaskScheduler.Core.TaskTypes.Recording.Builder;
using TaskScheduler.Web.Extensions;
using TaskScheduler.Web.Models.Recording;
using TaskScheduler.Web.Services.Interfaces;
using static System.Web.HttpUtility;

namespace TaskScheduler.Web.Services
{
    public class RecordingServices : IRecordingServices
    {
        private readonly ITaskSchedulerService<RecordingTask, RecordingModel> _recordingScheduler;

        public RecordingServices(ITaskSchedulerService<RecordingTask, RecordingModel> recordingScheduler)
        {
            _recordingScheduler = recordingScheduler;
        }

        public IEnumerable<RecordingViewModel> GetRecordings()
        {
            return _recordingScheduler.GetTasks().Select(t => t.ToViewModel()).ToList();
        }

        public IEnumerable<RecordingViewModel> GetSortedRecording(string sortBy)
        {
            var recordings = GetRecordings();

            switch (sortBy)
            {
                case "Title":
                    recordings = recordings.OrderBy(x => x.Title).ToList();
                    break;
                case "ChannelName":
                    recordings = recordings.OrderBy(x => x.ChannelName).ToList();
                    break;
                case "StartDate":
                    recordings = recordings.OrderBy(x => x.StartDate).ToList();
                    break;
                case "StartTime":
                    recordings = recordings.OrderBy(x => x.StartDate.ToString("HH:mm")).ToList();
                    break;
                case "EndTime":
                    recordings = recordings.OrderBy(x => x.EndDate.ToString("HH:mm")).ToList();
                    break;
            }

            return recordings;
        }

        public void AddRecording(RecordingViewModel recording)
        {
            var task = new RecordingTaskBuilder()
                    .SetTitle(recording.Title)
                    .SetChannel(recording.ChannelID)
                    .SetProgrammeStartTime(recording.StartDate.SetTime(recording.StartTime))
                    .SetProgrammeEndTime(recording.EndDate.SetTime(recording.EndTime))
                    .SetRecurrence(recording.Recurrence, recording.RecurrenceEndDate)
                    .SetEnabled(recording.IsEnabled)
                    .Build();
            _recordingScheduler.AddTask(task);
        }

        public RecordingViewModel GetRecording(string id)
        {
            return _recordingScheduler.GetTask(id)?.ToViewModel();
        }

        public void UpdateRecording(RecordingViewModel recording)
        {
            _recordingScheduler.DeleteTask(recording.PreviousTitle);
            AddRecording(recording);
        }

        public void DeleteRecording(RecordingViewModel recording)
        {
            _recordingScheduler.DeleteTask(UrlDecode(recording.Title));
        }
    }
}