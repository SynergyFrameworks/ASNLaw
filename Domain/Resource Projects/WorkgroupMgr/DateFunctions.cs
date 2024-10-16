using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace WorkgroupMgr
{
    public static class DateFunctions
    {
        public enum DateInterval { Second, Minute, Hour, Day, Week, Month, Quarter, Year }

        public static int DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            int lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);

            switch (Interval)
            {
                case DateInterval.Day:
                    lngDateDiffValue = (int)TS.Days;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (int)TS.TotalHours;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (int)TS.TotalMinutes;
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (int)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (int)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Second:
                    lngDateDiffValue = (int)TS.TotalSeconds;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (int)(TS.Days / 7);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (int)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }
    }
}
