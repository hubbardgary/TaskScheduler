using System;
using TaskScheduler.Shared.Enums;
using TaskScheduler.Core.TaskTypes.Base;

namespace TaskScheduler.Core.TaskTypes.Recording.Builder
{
    public class BaseTaskBuilder
    {
        const string ChannelPlaceholder = "<channel>";
        const string DurationPlaceholder = "<duration>";
        readonly string ExecActionArguments = $@"/quitafterrecord /renderless=on /minimized /chid={ChannelPlaceholder} /duration={DurationPlaceholder}";

        private BaseTask _task;

        public BaseTaskBuilder()
        {
            _task = new BaseTask();
            _task.Model.IsEnabled = true;
            _task.Model.IsExpired = false;
        }

        public BaseTask Build()
        {
            return _task;
        }

        public BaseTaskBuilder SetName(string name)
        {
            _task.Model.Name = name;
            _task.Model.PreviousName = name;
            return this;
        }

        public BaseTaskBuilder SetIsEnabled(bool isEnabled)
        {
            _task.Model.IsEnabled = isEnabled;
            return this;
        }

        public BaseTaskBuilder SetIsRecurring(bool isRecurring)
        {
            _task.Model.IsRecurring = isRecurring;
            return this;
        }

        public BaseTaskBuilder SetRecurrence(RecurrenceType recurrence, DateTime recurrenceEndDate)
        {
            _task.Model.Recurrence = recurrence;
            _task.Model.RecurrenceEndDate = recurrenceEndDate;
            return this;
        }

        public BaseTaskBuilder SetStartDate(DateTime startDate)
        {
            _task.Model.StartDate = startDate;
            _task.Model.TriggerTime = $"At {_task.Model.StartDate.ToShortTimeString()} on {_task.Model.StartDate.ToShortDateString()}";
            return this;
        }
        
        public BaseTaskBuilder SetExecAction()
        {
            _task.Model.ExecAction = ExecAction;
            return this;
        }

        public BaseTaskBuilder SetExecActionArguments(int channelId, int duration)
        {
            _task.Model.ExecActionArguments = ExecActionArguments
                .Replace(ChannelPlaceholder, channelId.ToString())
                .Replace(DurationPlaceholder, duration.ToString());
            return this;
        }

        public BaseTaskBuilder SetDescription(string showTitle, string channelName, DateTime programmeStart, DateTime programmeEnd)
        {
            _task.Model.Description = 
                $"Recording {showTitle} on {channelName} " +
                $"from {programmeStart.ToShortDateString()} {programmeStart.ToShortTimeString()} " +
                $"to {programmeEnd.ToShortDateString()} {programmeEnd.ToShortTimeString()}.";

            return this;
        }

        public BaseTaskBuilder SetTriggerTime(DateTime recordingStart)
        {
            if(recordingStart == DateTime.MinValue)
            {
                throw new Exception("RecordingStart is not set");
            }

            _task.Model.TriggerTime = $"At {recordingStart.ToShortTimeString()} on {recordingStart.ToShortDateString()}";
            return this;
        }
    }
}
