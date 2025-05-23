﻿
namespace TODO.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime DateTimeConverter(string zonedTimeData)
        {
            if (string.IsNullOrEmpty(zonedTimeData))
            {
                throw new ArgumentNullException(nameof(zonedTimeData), "Date-time string cannot be null or empty.");
            }
            if (DateTimeOffset.TryParse(zonedTimeData, out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset.LocalDateTime;
            }
            throw new FormatException($"Invalid ZonedDateTime format: {zonedTimeData}");
        }

        public static string ToJavaZonedDateTime(DateTime dateTime)
        {
            var userOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
            var localDateTime = new DateTimeOffset(dateTime, userOffset);
            return localDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
        }
        
        public static DateTime ToDateTime(long millis)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(millis).DateTime;
        }
        
    }
}
