using System.Globalization;
using System.Windows.Data;

namespace TODO.Converter;

public class DateTimeToRelativeStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            DateTime date when date == DateTime.MinValue => "No deadline",
            DateTime date => date.Date switch
            {
                var d when d == DateTime.Today => "Today",
                var d when d == DateTime.Today.AddDays(1) => "Tomorrow",
                var d when d == DateTime.Today.AddDays(-1) => "Yesterday",
                _ => date.ToString("MMM dd, yyyy", culture)
            },
            _ => string.Empty
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}