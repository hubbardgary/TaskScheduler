using TaskScheduler.Web.Models.Enums;

namespace TaskScheduler.Web.Models.Recording
{
    public class RecordingIndexViewModel
    {
        public ActionOutcome ActionOutcome { get; set; }
        public PagedList.IPagedList<RecordingViewModel> Recordings { get; set; }
    }
}