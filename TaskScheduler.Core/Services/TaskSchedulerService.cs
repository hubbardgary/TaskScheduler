using Microsoft.Win32.TaskScheduler;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TaskScheduler.Core.Extensions;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.Services
{
    public abstract class TaskSchedulerService<T, TI> : ITaskSchedulerService<T, TI>
        where T : ITask<TI>, new()
        where TI : ITaskModel
    {
        public abstract string TaskFolder { get; set; }

        private readonly TaskService _taskService;
        private readonly ITaskFolderService _taskFolderService;
        private readonly ITaskTriggerBuilder _taskTriggerBuilder;

        protected TaskSchedulerService(
            TaskService taskService,
            ITaskFolderService taskFolderService,
            ITaskTriggerBuilder taskTriggerBuilder)
        {
            _taskService = taskService;
            _taskFolderService = taskFolderService;
            _taskTriggerBuilder = taskTriggerBuilder;
        }

        public void AddTask(ITask<TI> customTask)
        {
            AddTask(customTask.ToBaseTaskModel());
        }

        public void AddTask(BaseTask task)
        {
            var taskDefinition = _taskService.NewTask();
            taskDefinition.RegistrationInfo.Description = task.Model.Description;
            taskDefinition.Triggers.Add(_taskTriggerBuilder.BuildTrigger(task.Model));
            taskDefinition.Actions.Add(new ExecAction(task.Model.ExecAction, task.Model.ExecActionArguments));

            if (!_taskFolderService.FolderExists(TaskFolder))
            {
                _taskFolderService.CreateFolder(TaskFolder);
            }
            
            taskDefinition.Settings.DisallowStartIfOnBatteries = false;
            taskDefinition.Settings.StopIfGoingOnBatteries = false;
            taskDefinition.Settings.Enabled = task.Model.IsEnabled;

            AddTask(task.Model.Name, taskDefinition);
        }

        private void AddTask(string taskName, TaskDefinition td)
        {
            if (IsDupe(taskName))
                throw new System.Exception("A task with that name already exists");
            _taskService.GetFolder(TaskFolder).RegisterTaskDefinition(taskName, td);
        }

        private bool IsDupe(string name)
        {
            if (GetTask(name) != null)
                return true;

            return false;
        }

        public void DeleteTask(string id)
        {
            TaskFolder folder = _taskFolderService.GetFolder(TaskFolder);
            if (folder != null)
            {
                if (GetTask(id) != null)
                {
                    folder.DeleteTask(id);
                }
            }
        }

        public void UpdateTask(T customTask)
        {
            var task = customTask.ToBaseTaskModel();
            if (task.Model.NameHasChanged && IsDupe(task.Model.Name))
                throw new System.Exception("Task with that name already exists");

            DeleteTask(task.Model.PreviousName);
            AddTask(task);
        }

        public T GetTask(string taskName)
        {
            var name = new Regex($"^{Regex.Escape(taskName)}$", RegexOptions.Compiled);

            TaskFolder folder = _taskFolderService.GetFolder(TaskFolder);
            if (folder != null)
            {
                var task = folder.GetTasks(name).FirstOrDefault()?.ToWindowsTask();

                if (task != null)
                {
                    return (T)new T().ToCustomTaskModel(task);
                }
            }
            return default(T);
        }

        public IEnumerable<T> GetTasks()
        {
            TaskFolder folder = _taskFolderService.GetFolder(TaskFolder);
            if (folder != null)
            {
                return folder.GetTasks().Select(t => (T)new T().ToCustomTaskModel(t.ToWindowsTask()));
            }
            return new List<T>();
        }
    }
}
