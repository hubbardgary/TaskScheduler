using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Recording;
using TaskScheduler.Core.TaskTypes.Base;
using TaskScheduler.Core.TaskTypes.Recording.Builder;
using TaskScheduler.Core.TaskTypes.Recording.Extensions;

namespace TaskScheduler.Core.TaskTypes.Recording
{
    public class RecordingTask : ITask<RecordingModel>
    {
        public RecordingModel Model { get; set; }

        public ITask<RecordingModel> ToCustomTaskModel(BaseTask task)
        {
            var builder = new RecordingTaskBuilder();
            return builder
                .SetPreviousTitle(task.Model.Name)
                .SetTitle(task.Model.Name)
                .SetRecordingStartTime(task.GetRecordingStart())
                .SetRecordingEndTime(task.GetRecordingStart().AddMinutes(task.GetDuration()))
                .SetChannel(task.GetChannel())
                .Build();
        }

        public BaseTask ToBaseTaskModel()
        {
            var builder = new BaseTaskBuilder();
            return builder
                .SetName(Model.Title)
                .SetIsEnabled(true)
                .SetRecurrence(Enums.RecurrenceType.OneOff)
                .SetStartDate(Model.RecordingStart)
                .SetExecAction(Model.Channel.Id, Model.RecordingDurationMinutes)
                .SetTriggerTime(Model.RecordingStart)
                .SetDescription(Model.Title, Model.Channel.Name, Model.ProgrammeStart, Model.ProgrammeEnd)
                .Build();
        }
    }
}
