using System;
using System.Collections.Generic;
using System.Linq;

namespace ReswareOrderMonitorService.Utilities
{
    internal class DateTimeUtility : IDateTimeUtility
    {
        private const int StartingHour = 8;
        private const int EndingHour = 20;
        private readonly ICollection<DayOfWeek> _weekendDays = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday};

        public DateTime ResolveTitleOpinionClosingDateTime(DateTime closingDateTime)
        {
            var holidays = GetPcnHolidaysBasedOnClosingDateYear(closingDateTime.Date.Year);

            var endHour = closingDateTime.Date.AddHours(EndingHour - 1).AddMinutes(59).AddSeconds(59);
            var startHour = closingDateTime.Date.AddHours(StartingHour);

            if (_weekendDays.Contains(closingDateTime.Date.DayOfWeek) || holidays.Contains(closingDateTime.Date))
            {
                closingDateTime = startHour;
            }

            if (closingDateTime > endHour)
            {
                closingDateTime = closingDateTime.AddDays(1).AddHours(StartingHour);
            }
            else if (closingDateTime < startHour)
            {
                closingDateTime = closingDateTime.AddHours(StartingHour);
            }

            closingDateTime = AdjustForWeekendAndHoliday(closingDateTime, holidays);

            closingDateTime = closingDateTime.AddDays(1);
            return AdjustForWeekendAndHoliday(closingDateTime, holidays);
        }

        public DateTime ResolveDocPrepClosingDateTime()
        {
            var returnedTime = DateTime.Now;
            var dayOfTheWeek = returnedTime.DayOfWeek;

            if (DateTime.Now.Hour < 8)
            {
                return DateTime.Now.Date.AddHours(12);
            }

            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 16)
            {
                return DateTime.Now.AddHours(4);
            }

            if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 20)
            {
                return DateTime.Now.DayOfWeek == DayOfWeek.Friday ? DateTime.Now.AddHours(16).AddDays(2) : DateTime.Now.AddHours(16);
            }

            if (DateTime.Now.Hour < 20) return DateTime.Now.AddHours(4);

            return dayOfTheWeek == DayOfWeek.Friday ? DateTime.Now.Date.AddHours(12).AddDays(1).AddDays(2) : DateTime.Now.Date.AddHours(12).AddDays(1);
        }

        private static ICollection<DateTime> GetPcnHolidaysBasedOnClosingDateYear(int year)
        {
            var holidays = new List<DateTime>
            {
                new DateTime(year, 1, 1).Date,
                new DateTime(year, 7, 4).Date,
                new DateTime(year, 12, 25).Date
            };

            var memorialDay = new DateTime(year, 5, 31);
            var dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            holidays.Add(memorialDay.Date);


            var laborDay = new DateTime(year, 9, 1);
            dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            holidays.Add(laborDay.Date);

            var thanksgiving = (Enumerable.Range(1, 30).Where(day => new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday)).ElementAt(3);
            var thanksgivingDay = new DateTime(year, 11, thanksgiving);
            holidays.Add(thanksgivingDay.Date);

            return holidays;
        }

        private DateTime AdjustForWeekendAndHoliday(DateTime closingDateTime, ICollection<DateTime> holidays)
        {
            while (_weekendDays.Contains(closingDateTime.DayOfWeek) || holidays.Contains(closingDateTime.Date))
            {
                closingDateTime = closingDateTime.AddDays(1);
            }
            return closingDateTime;
        }


    }
}
