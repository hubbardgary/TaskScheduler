using TaskScheduler.Web.Models.Enums;

namespace TaskScheduler.Web.Models.Shutdown
{
    public class ShutdownIndexViewModel
    {
        public ActionOutcome ActionOutcome { get; set; }
        public PagedList.IPagedList<ShutdownViewModel> Shutdowns { get; set; }
}
}