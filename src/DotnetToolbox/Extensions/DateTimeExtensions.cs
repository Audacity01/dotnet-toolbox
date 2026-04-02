using System;

namespace DotnetToolbox.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime date) => date.Date == DateTime.Today;

        public static bool IsWeekday(this DateTime date) =>
            date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

        public static DateTime EndOfDay(this DateTime date) =>
            date.Date.AddDays(1).AddTicks(-1);

        public static DateTime StartOfMonth(this DateTime date) =>
            new DateTime(date.Year, date.Month, 1);

        public static DateTime EndOfMonth(this DateTime date) =>
            date.StartOfMonth().AddMonths(1).AddTicks(-1);

        public static int Age(this DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static DateTime NextWeekday(this DateTime date)
        {
            var next = date.AddDays(1);
            while (!next.IsWeekday())
                next = next.AddDays(1);
            return next;
        }
    }
}
