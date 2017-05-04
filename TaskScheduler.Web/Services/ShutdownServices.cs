﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaskScheduler.Core.Interfaces;
using TaskScheduler.Core.Models.Shutdown;
using TaskScheduler.Core.TaskTypes.Shutdown;
using TaskScheduler.Core.TaskTypes.Shutdown.Builder;
using TaskScheduler.Web.Extensions;
using TaskScheduler.Web.Models.Recording;
using TaskScheduler.Web.Models.Shutdown;
using TaskScheduler.Web.Services.Interfaces;
using static System.Web.HttpUtility;

namespace TaskScheduler.Web.Services
{
    public class ShutdownServices : IShutdownServices
    {
        private readonly ITaskSchedulerService<ShutdownTask, ShutdownModel> _shutdownScheduler;

        public ShutdownServices(ITaskSchedulerService<ShutdownTask, ShutdownModel> shutdownScheduler)
        {
            _shutdownScheduler = shutdownScheduler;
        }

        public void AddShutdown(ShutdownViewModel shutdown)
        {
            var task = new ShutdownTaskBuilder()
                .SetName(shutdown.Name)
                .SetPreviousName(shutdown.PreviousName)
                .SetShutdownDateTime(shutdown.ShutdownDate.AddHours(shutdown.ShutdownTime.Hour).AddMinutes(shutdown.ShutdownTime.Minute))
                .SetRecurrence(shutdown.Recurrence)
                .SetRecurrenceEndDate(shutdown.RecurrenceEndDate)
                .SetEnabled(shutdown.IsEnabled)
                .Build();
            _shutdownScheduler.AddTask(task);
        }

        public void DeleteShutdown(ShutdownViewModel shutdown)
        {
            _shutdownScheduler.DeleteTask(UrlDecode(shutdown.Name));
        }

        public ShutdownViewModel GetShutdown(string id)
        {
            return _shutdownScheduler.GetTask(id)?.ToViewModel();
        }

        public IEnumerable<ShutdownViewModel> GetShutdowns()
        {
            return _shutdownScheduler.GetTasks().Select(t => t.ToViewModel()).ToList();
        }

        public IEnumerable<ShutdownViewModel> GetSortedShutdowns(string sortBy)
        {
            var shutdowns = GetShutdowns();

            switch (sortBy)
            {
                case "Name":
                    shutdowns = shutdowns.OrderBy(x => x.Name).ToList();
                    break;
                case "ShutdownDateTime":
                    shutdowns = shutdowns.OrderBy(x => x.ShutdownDate).ToList();
                    break;
            }

            return shutdowns;
        }

        public void UpdateShutdown(ShutdownViewModel shutdown)
        {
            _shutdownScheduler.DeleteTask(shutdown.PreviousName);
            AddShutdown(shutdown);
        }

        public void CreateShutdownFromRecording(RecordingViewModel recording)
        {
            var shutdownTask = BuildLinkedShutdownTask(recording);
            _shutdownScheduler.AddTask(shutdownTask);
        }

        public void DeleteLinkedShutdown(RecordingViewModel recording)
        {
            _shutdownScheduler.DeleteTask($"{recording.Title}#recording");
        }

        public void UpdateLinkedShutdown(RecordingViewModel recording)
        {
            var shutdownTask = BuildLinkedShutdownTask(recording);
            _shutdownScheduler.UpdateTask(shutdownTask);
        }

        private ShutdownTask BuildLinkedShutdownTask(RecordingViewModel recording)
        {
            return new ShutdownTaskBuilder()
                .SetName($"{recording.Title}#recording")
                .SetPreviousName($"{recording.PreviousTitle}#recording")
                .SetShutdownDateTime(recording.EndDate.AddHours(recording.EndTime.Hour).AddMinutes(recording.EndTime.Minute).AddMinutes(6))
                .SetRecurrence(recording.Recurrence)
                .SetRecurrenceEndDate(recording.RecurrenceEndDate)
                .SetEnabled(recording.IsEnabled)
                .Build();
        }
    }
}