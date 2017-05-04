using System.Collections.Generic;
using TaskScheduler.Web.Models.Recording;
using TaskScheduler.Web.Models.Shutdown;

namespace TaskScheduler.Web.Services.Interfaces
{
    public interface IShutdownServices
    {
        IEnumerable<ShutdownViewModel> GetShutdowns();
        IEnumerable<ShutdownViewModel> GetSortedShutdowns(string sortBy);
        void AddShutdown(ShutdownViewModel shutdown);
        ShutdownViewModel GetShutdown(string id);
        void UpdateShutdown(ShutdownViewModel shutdown);
        void DeleteShutdown(ShutdownViewModel shutdown);
        void CreateShutdownFromRecording(RecordingViewModel recording);
        void DeleteLinkedShutdown(RecordingViewModel recording);
        void UpdateLinkedShutdown(RecordingViewModel recording);
    }
}
