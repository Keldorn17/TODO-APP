using System.Globalization;
using System.Windows.Data;

namespace TODO.Converter;

public class DateTimeToRelativeStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            var today = DateTime.Today;
            if (date.Date == today)
                return "Today";
            if (date.Date == today.AddDays(1))
                return "Tomorrow";
            if (date.Date == today.AddDays(-1))
                return "Yesterday";
            return date.ToString("MMM dd, yyyy");
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}