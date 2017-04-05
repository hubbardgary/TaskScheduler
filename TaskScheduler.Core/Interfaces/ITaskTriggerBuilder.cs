using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Models.Base;

namespace TaskScheduler.Core.Interfaces
{
    public interface ITaskTriggerBuilder
    {
        Trigger BuildTrigger(BaseTaskModel task);
    }
}
