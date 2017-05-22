using System.Configuration;

namespace TaskScheduler.Core.Config
{
    public static class CustomTaskSettings
    {
        public static string RecordingsFolder { get; set; }
        public static int RecordingPaddingInMinutes { get; set; }
        public static string ShutdownsFolder { get; set; }
        public static string LinkedTaskSuffix { get; set; }
        public static int LinkedShutdownDelayInMinutes { get; set; }

        static CustomTaskSettings()
        {
            RecordingsFolder = ConfigurationManager.AppSettings["RecordingsFolder"];
            RecordingPaddingInMinutes = int.Parse(ConfigurationManager.AppSettings["RecordingStartAndEndPaddingInMinutes"]);
            ShutdownsFolder = ConfigurationManager.AppSettings["ShutdownsFolder"];
            LinkedTaskSuffix = ConfigurationManager.AppSettings["LinkedTaskSuffix"];
            LinkedShutdownDelayInMinutes = int.Parse(ConfigurationManager.AppSettings["LinkedShutdownDelayInMinutes"]);
        }
    }
}
