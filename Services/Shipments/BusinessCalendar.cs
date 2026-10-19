namespace AitkenShipping.Services.Shipments;

/// <summary>
/// Adds transit time in business days, skipping weekends, so estimated
/// delivery dates never land on a Saturday or Sunday.
/// </summary>
public static class BusinessCalendar
{
    public static DateTime AddBusinessDays(DateTime start, int businessDays)
    {
        DateTime date = start;
        int added = 0;

        while (added < businessDays)
        {
            date = date.AddDays(1);
            if (IsWeekday(date))
            {
                added++;
            }
        }

        return date;
    }

    private static bool IsWeekday(DateTime date) =>
        date.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday);
}
