using System;
using System.Globalization;

namespace Pockit.ChatBot.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetWeekOfYear(this DateTime date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNumber = currentCulture.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Saturday);

            return weekNumber;
        }
    }
}
