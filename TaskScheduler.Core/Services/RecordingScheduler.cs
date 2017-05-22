using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.TaskTypes.Recording;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Config;

namespace TaskScheduler.Core.Services
{
    public class RecordingScheduler : TaskSchedulerService<RecordingTask, RecordingModel>
    {
        public override string TaskFolder { get; set; } = CustomTaskSettings.RecordingsFolder;

        public RecordingScheduler(TaskService taskService, ITaskFolderService taskFolderService, ITaskTriggerBuilder taskTriggerBuilder) 
            : base(taskService, taskFolderService, taskTriggerBuilder) { }
    }
}
