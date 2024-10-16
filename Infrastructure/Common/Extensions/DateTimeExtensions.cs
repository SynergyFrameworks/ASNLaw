using System;

namespace Infrastructure.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static int Quarter(this DateTime date)
        {
            if (date.Month < 4)
                return 1;
            if (date.Month < 7)
                return 2;
            if (date.Month < 10)
                return 3;
            return 4;
        }

        public static decimal GetYearDifference(this DateTime startDate, DateTime endDate)
        {
            var difference = endDate - startDate;
            return difference.Days / 365;

        }

    }
}
