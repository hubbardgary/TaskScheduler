using Microsoft.Win32.TaskScheduler;
using TaskScheduler.Core.Services;
using System;
using TaskScheduler.Core.TaskTypes.Recording.Builder;
using TaskScheduler.Core.TaskTypes.Shutdown.Builder;

namespace TaskScheduler.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskService = new TaskService();
            var taskFolderService = new TaskFolderService(taskService);
            var taskTriggerBuilder = new TaskTriggerBuilder();
            var recordingService = new RecordingScheduler(taskService, taskFolderService, taskTriggerBuilder);
            var shutdownService = new ShutdownScheduler(taskService, taskFolderService, taskTriggerBuilder);

            recordingService.DeleteTask("title");

            var recordingTask = new RecordingTaskBuilder()
                .SetTitle("title")
                .SetChannel(42)
                .SetProgrammeStartTime(new DateTime(2016, 11, 30, 18, 0, 0))
                .SetProgrammeEndTime(new DateTime(2016, 11, 30, 19, 0, 0))
                .Build();
            
            recordingService.AddTask(recordingTask);

            var task = recordingService.GetTask("title");

            var existingShutdownTask = shutdownService.GetTask("Test shutdown");

            shutdownService.DeleteTask("Test shutdown");

            var shutdownTask = new ShutdownTaskBuilder()
                .SetName("Test shutdown")
                .SetShutdownDateTime(DateTime.Now.AddMinutes(30))
                .Build();

            shutdownService.AddTask(shutdownTask);

            shutdownService.DeleteTask("Test shutdown");

            //var tasks = recordingService.GetTasks();
            //foreach (var t in tasks)
            //{
            //    var a = t;
            //}
        }
    }
}
