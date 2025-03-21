using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace TODO.Utils
{
    public static class ButtonUtils
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached("ImageSource", typeof(ImageSource), typeof(ButtonUtils), new PropertyMetadata(null));

        public static void SetImageSource(UIElement element, ImageSource value)
        {
            element.SetValue(ImageSourceProperty, value);
        }

        public static ImageSource GetImageSource(UIElement element)
        {
            return (ImageSource)element.GetValue(ImageSourceProperty);
        }
    }

    public class HoverButton : Button
    {

        public static readonly DependencyProperty hoverColorProperty = DependencyProperty.Register
            (
                 "hoverColor",
                 typeof(SolidColorBrush),
                 typeof(HoverButton),
                 new PropertyMetadata(new BrushConverter().ConvertFrom("#737373"))
            );

        public SolidColorBrush hoverColor
        {
            get { return (SolidColorBrush)GetValue(hoverColorProperty); }
            set { SetValue(hoverColorProperty, value); }
        }

        public static readonly DependencyProperty bgColorProperty = DependencyProperty.Register
         (
              "bgColor",
              typeof(SolidColorBrush),
              typeof(HoverButton),
              new PropertyMetadata(new SolidColorBrush(Colors.Red))
         );

        public SolidColorBrush bgColor
        {
            get { return (SolidColorBrush)GetValue(bgColorProperty); }
            set { SetValue(bgColorProperty, value); }
        }
    }
}
