
namespace TODO.Utils
{
    public static class DateTimeUtils
    {
        public static DateTimeOffset DateTimeConverter(string zonedTimeData)
        {
            if (string.IsNullOrEmpty(zonedTimeData))
            {
                throw new ArgumentNullException(nameof(zonedTimeData), "Date-time string cannot be null or empty.");
            }
            if (DateTimeOffset.TryParse(zonedTimeData, out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset.LocalDateTime;
            }
            else
            {
                throw new FormatException("Invalid date-time format.");
            }
        }
    }
}
