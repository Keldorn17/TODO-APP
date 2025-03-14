using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        return Brushes.Green;
                    case 2:
                        return Brushes.Yellow;
                    case 3:
                        return Brushes.Orange;
                    case 4:
                        return Brushes.Red;
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
