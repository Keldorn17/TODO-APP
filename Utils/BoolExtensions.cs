using System.Windows;

namespace TODO.Utils;

public static class BoolExtensions
{
    public static Visibility ToVisibility(this bool value) =>
        value ? Visibility.Visible : Visibility.Collapsed;
}