using System;

namespace PapaBlog.Shared.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderScore(this DateTime dateTime)
        {
            dateTime = DateTime.Now;
            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}