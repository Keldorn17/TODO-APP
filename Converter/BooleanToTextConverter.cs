using System.Globalization;
using System.Windows.Data;

namespace TODO.Converter;

public class BooleanToTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (bool)value! ? "Completed" : "Complete";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}