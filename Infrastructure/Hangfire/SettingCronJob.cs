using System;
using Hangfire.States;
using Hangfire.Common;
using Infrastructure.Settings;

namespace Hangfire
{
    public class SettingCronJob
    {
        internal SettingCronJob() { }

        public string RecurringJobId { get; set; }
        public SettingDescriptor EnableSetting { get; set; }
        public Func<object, bool> EnabledEvaluator { get; set; } = x => (bool)x;
        public SettingDescriptor CronSetting { get; set; }
        public Job Job { get; set; }
        public string Queue { get; set; } = EnqueuedState.DefaultQueue;
        public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Utc;
    }
}
