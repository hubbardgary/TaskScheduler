using System;
using TaskScheduler.Core.Models.Recording;

namespace TaskScheduler.Core.TaskTypes.Recording.Builder
{
    public class RecordingTaskBuilder
    {
        private RecordingModel _task;

        public RecordingTaskBuilder()
        {
            _task = new RecordingModel();
        }

        public RecordingTask Build()
        {
            return new RecordingTask
            {
                Model = _task
            };
        }

        public RecordingTaskBuilder SetTitle(string title)
        {
            _task.Title = title;
            return this;
        }

        public RecordingTaskBuilder SetPreviousTitle(string prevTitle)
        {
            _task.PreviousTitle = prevTitle;
            return this;
        }

        public RecordingTaskBuilder SetProgrammeStartTime(DateTime start)
        {
            _task.ProgrammeStart = start;
            _task.RecordingStart = start.AddMinutes(-Constants.TimeBufferInMinutes);
            return this;
        }

        public RecordingTaskBuilder SetProgrammeEndTime(DateTime end)
        {
            _task.ProgrammeEnd = end;
            _task.RecordingEnd = end.AddMinutes(Constants.TimeBufferInMinutes);
            return this;
        }

        public RecordingTaskBuilder SetRecordingStartTime(DateTime start)
        {
            _task.RecordingStart = start;
            _task.ProgrammeStart = start.AddMinutes(Constants.TimeBufferInMinutes);
            return this;
        }

        public RecordingTaskBuilder SetRecordingEndTime(DateTime end)
        {
            _task.RecordingEnd = end;
            _task.ProgrammeEnd = end.AddMinutes(-Constants.TimeBufferInMinutes);
            return this;
        }

        public RecordingTaskBuilder SetChannel(int channelId)
        {
            _task.Channel = new ChannelModel(channelId);
            return this;
        }

        public RecordingTaskBuilder SetChannel(string channelName)
        {
            _task.Channel = new ChannelModel(channelName);
            return this;
        }

        public RecordingTaskBuilder SetChannel(ChannelModel channel)
        {
            _task.Channel = channel;
            return this;
        }
    }
}
