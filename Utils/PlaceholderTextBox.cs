using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace TODO.Utils
{
    public class PlaceholderTextBox : TextBox
    {
        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox),
                new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(PlaceholderTextBox),
                new PropertyMetadata(string.Empty));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("TitleIcon", typeof(Geometry), typeof(PlaceholderTextBox));

        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}
