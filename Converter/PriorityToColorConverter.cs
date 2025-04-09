using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TODO.Converter
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not int priorityLevel) return Brushes.Transparent;
            return priorityLevel switch
            {
                0 => Brushes.Transparent,
                1 => Application.Current.Resources["PriorityGreen"],
                2 => Application.Current.Resources["PriorityYellow"],
                3 => Application.Current.Resources["PriorityOrange"],
                4 => Application.Current.Resources["PriorityRed"],
                _ => Brushes.Transparent
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
