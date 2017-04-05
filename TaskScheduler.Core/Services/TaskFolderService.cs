using Microsoft.Win32.TaskScheduler;
using System.Linq;
using TaskScheduler.Core.Interfaces;

namespace TaskScheduler.Core.Services
{
    public class TaskFolderService : ITaskFolderService
    {
        private TaskService _taskService;

        public TaskFolderService(TaskService taskService)
        {
            _taskService = taskService;
        }

        public bool FolderExists(string folderName)
        {
            return (GetFolder(folderName) != null);
        }

        public TaskFolder GetFolder(string folderName)
        {
            return _taskService.RootFolder.SubFolders.FirstOrDefault(f => f.Name == folderName);
        }

        public void CreateFolder(string folderName)
        {
            _taskService.RootFolder.CreateFolder(folderName);
        }
    }
}
