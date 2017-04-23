using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Core.TaskTypes.Shutdown;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Services
{
    public class ShutdownScheduler : TaskSchedulerService<ShutdownTask, ShutdownModel>
    {
        public override string TaskFolder { get; set; } = "Shutdown";

        public ShutdownScheduler(TaskService taskService, ITaskFolderService taskFolderService, ITaskTriggerBuilder taskTriggerBuilder) 
            : base(taskService, taskFolderService, taskTriggerBuilder) { }
    }
}
