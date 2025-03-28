using System.Windows;
using System.Windows.Controls;

namespace TODO.Utils
{
    public class DropdownMenu : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(DropdownMenu), new PropertyMetadata(false));

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        static DropdownMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownMenu), new FrameworkPropertyMetadata(typeof(DropdownMenu)));
        }
    }
}
