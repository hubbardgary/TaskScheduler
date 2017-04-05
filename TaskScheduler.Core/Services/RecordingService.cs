using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.TaskTypes.Recording;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Services
{
    public class RecordingService : TaskSchedulerService<RecordingTask, RecordingModel>
    {
        public override string TaskFolder { get; set; } = "Recordings";

        public RecordingService(TaskService taskService, ITaskFolderService taskFolderService, ITaskTriggerBuilder taskTriggerBuilder) 
            : base(taskService, taskFolderService, taskTriggerBuilder) { }
    }
}
