using System;
using System.Text.RegularExpressions;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.TaskTypes.Recording.Extensions
{
    public static class RecordingBaseTaskExtensions
    {
        public static int GetDuration(this BaseTask task)
        {
            const string RegexDuration = @"duration=(?<duration>\d+)";
            int duration;
            int.TryParse(Regex.Match(task.Model.ExecAction, RegexDuration).Groups["duration"].Value, out duration);
            return duration;
        }
        
        public static ChannelModel GetChannel(this BaseTask task)
        {
            const string RegexChannelFromTaskDescription = @"Record(ing)? .* on (?<channel>[a-zA-Z0-9+ */]+) (from|at)";

            // TODO: What should really happen here?
            if (string.IsNullOrEmpty(task.Model.Description))
                return new ChannelModel(0);

            return new ChannelModel(Regex.Match(task.Model.Description, RegexChannelFromTaskDescription).Groups["channel"].Value);
        }

        public static DateTime GetRecordingStart(this BaseTask task)
        {
            const string RegexStartTimeFromTaskDescription = @"at (?<time>[0-9][0-9]:[0-9][0-9]) on (?<date>[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9])";
            var matches = Regex.Match(task.Model.TriggerTime, RegexStartTimeFromTaskDescription, RegexOptions.IgnoreCase);

            string dateMatch = matches.Groups["date"].Value;
            string timeMatch = matches.Groups["time"].Value;

            DateTime startDate;

            DateTime.TryParse($"{dateMatch} {timeMatch}", out startDate);

            if (startDate > DateTime.MinValue)
            {
                return startDate;
            }

            return task.Model.StartDate;
        }
    }
}
