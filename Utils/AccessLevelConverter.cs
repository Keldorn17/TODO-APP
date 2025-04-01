using System.Globalization;
using System.Windows.Data;

namespace TODO.Utils
{
    public class AccessLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return AccessLevel.GetByIndex(intValue);
            }
            return AccessLevel.Read;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AccessLevel accessLevel)
            {
                return accessLevel.Index;
            }
            return 0;
        }
    }
}
