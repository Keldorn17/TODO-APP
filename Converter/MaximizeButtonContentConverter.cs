using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TODO.Converter;

public class MaximizeButtonContentConverter : IValueConverter
{
    private const string RestoreButton = "❐";
    private const string MaximizeButton = "▢";
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is WindowState state)
        {
            return state == WindowState.Maximized ? RestoreButton : MaximizeButton;
        }
        return MaximizeButton;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}