﻿using Microsoft.Win32.TaskScheduler;
using System;
using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Extensions;

namespace TaskScheduler.Core.Services
{
    public class TriggerFactory
    {
        public static Trigger GetTrigger(DateTime triggerDate, RecurrenceType recurrence)
        {
            switch (recurrence)
            {
                case RecurrenceType.Daily:
                    return new WeeklyTrigger
                    {
                        WeeksInterval = 1,
                        DaysOfWeek = DaysOfTheWeek.AllDays
                    };
                case RecurrenceType.Weekly:
                    return new WeeklyTrigger
                    {
                        WeeksInterval = 1,
                        DaysOfWeek = triggerDate.ToTaskDay()
                    };
                case RecurrenceType.WeekDays:
                    return new WeeklyTrigger
                    {
                        WeeksInterval = 1,
                        DaysOfWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Tuesday | DaysOfTheWeek.Wednesday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
                    };
                default:
                    // One off task
                    return new TimeTrigger();
            }
        }
    }
}
