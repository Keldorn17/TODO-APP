using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace TODO.Utils
{
    public class HoverButton : Button
    {

        public static readonly DependencyProperty HoverColorProperty = DependencyProperty.Register
            (
                 nameof(HoverColor),
                 typeof(SolidColorBrush),
                 typeof(HoverButton),
                 new PropertyMetadata(new BrushConverter().ConvertFrom("#737373"))
            );

        public SolidColorBrush HoverColor
        {
            get => (SolidColorBrush)GetValue(HoverColorProperty);
            set => SetValue(HoverColorProperty, value);
        }

        public static readonly DependencyProperty BgColorProperty = DependencyProperty.Register
         (
              nameof(BgColor),
              typeof(SolidColorBrush),
              typeof(HoverButton),
              new PropertyMetadata(new SolidColorBrush(Colors.Red))
         );

        public SolidColorBrush BgColor
        {
            get => (SolidColorBrush)GetValue(BgColorProperty);
            set => SetValue(BgColorProperty, value);
        }
    }
}
