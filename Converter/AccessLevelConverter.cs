using System.Globalization;
using System.Windows.Data;
using TODO.Domain;

namespace TODO.Converter
{
    public class AccessLevelConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return AccessLevel.GetByIndex(intValue);
            }
            return AccessLevel.GetByIndex(0);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is AccessLevel accessLevel)
            {
                return accessLevel.Index;
            }
            return 0;
        }
    }
}
