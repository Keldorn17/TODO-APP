using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TODO.Utils
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int priorityLevel)
            {
                switch (priorityLevel)
                {
                    case 0:
                        return Brushes.Transparent;
                    case 1:
                        return (SolidColorBrush)App.Current.Resources["PriorityGreen"];
                    case 2:
                        return (SolidColorBrush)App.Current.Resources["PriorityYellow"];
                    case 3:
                        return (SolidColorBrush)App.Current.Resources["PriorityOrange"];
                    case 4:
                        return (SolidColorBrush)App.Current.Resources["PriorityRed"];
                    default:
                        return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
