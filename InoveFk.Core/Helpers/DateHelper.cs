using System.Globalization;

namespace InoveFk.Core.Helpers;

public static class DateHelper
{
    public static bool IsCurrentDay(DayOfWeek day)
    {
        DayOfWeek currentDayOfWeek = DateTime.Now.DayOfWeek;
        return currentDayOfWeek == day;
    }

    public static IEnumerable<DateOnly> GetLastSevenDays()
    {
        DateTime currentDate = DateTime.UtcNow;
        List<DateOnly> lastSevenDays = [];

        for (int i = 0; i < 7; i++)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(currentDate.AddDays(-i));
            lastSevenDays.Add(dateOnly);
        }

        return lastSevenDays;
    }

    public static IEnumerable<DateOnly> GetDatesOfCurrentWeek()
    {
        List<DateOnly> datesOfWeek = [];

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
        DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        int difference = (7 + (currentDate.DayOfWeek - firstDayOfWeek)) % 7;
        DateOnly firstDayOfTheWeek = currentDate.AddDays(-difference);

        for (int i = 0; i < 7; i++)
        {
            datesOfWeek.Add(firstDayOfTheWeek.AddDays(i));
        }

        return datesOfWeek;
    }
}
