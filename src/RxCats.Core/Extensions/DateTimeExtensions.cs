using System.Globalization;

namespace RxCats.Core.Extensions;

public static class DateTimeExtensions
{
    public static int GetWeekOfYear(this DateTime dt)
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;

        var weekOfYear = dfi.Calendar.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

        return int.Parse($"{dt.Year}{weekOfYear:D2}");
    }
}