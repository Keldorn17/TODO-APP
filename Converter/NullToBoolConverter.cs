using System.Globalization;
using System.Windows.Data;

namespace TODO.Converter
{
    public class NullToBoolConverter : IValueConverter
    {
        public bool NullValue { get; set; } = false;
        public bool NonNullValue { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? NullValue : NonNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
