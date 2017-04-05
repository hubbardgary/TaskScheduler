using Microsoft.Win32.TaskScheduler;

namespace TaskScheduler.Core.Interfaces
{
    public interface ITaskFolderService
    {
        void CreateFolder(string folderName);
        bool FolderExists(string folderName);
        TaskFolder GetFolder(string folderName);
    }
}
