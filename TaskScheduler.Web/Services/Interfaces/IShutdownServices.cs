using System.Collections.Generic;
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
    }
}
