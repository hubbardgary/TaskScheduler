using System.Collections.Generic;
using TaskScheduler.Web.Models.Recording;

namespace TaskScheduler.Web.Services.Interfaces
{
    public interface IRecordingServices
    {
        IEnumerable<RecordingViewModel> GetRecordings();
        IEnumerable<RecordingViewModel> GetSortedRecording(string sortBy);
        void AddRecording(RecordingViewModel recording);
        RecordingViewModel GetRecording(string id);
        void UpdateRecording(RecordingViewModel recording);
        void DeleteRecording(RecordingViewModel recording);
    }
}
