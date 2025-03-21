using System;
using System.Linq;
using System.Windows;

namespace TODO.Utils
{
    public static class ThemeManager
    {
        private static string _currentTheme = "DarkTheme";

        public static void ToggleTheme()
        {
            var newTheme = _currentTheme == "DarkTheme" ? "LightTheme" : "DarkTheme";
            ApplyTheme(newTheme);
            _currentTheme = newTheme;
        }

        public static void ApplyTheme(string themeName)
        {
            var app = Application.Current;
            if (app == null) return;

            var mergedDictionaries = app.Resources.MergedDictionaries;
            var existingThemeDict = mergedDictionaries
                .FirstOrDefault(d => d.Source != null &&
                                    (d.Source.OriginalString.EndsWith("DarkTheme.xaml") ||
                                     d.Source.OriginalString.EndsWith("LightTheme.xaml")));

            if (existingThemeDict != null)
            {
                mergedDictionaries.Remove(existingThemeDict);
            }

            var themeUri = new Uri($"/Themes/ColorThemes/{themeName}.xaml", UriKind.Relative);
            var themeDict = new ResourceDictionary { Source = themeUri };
            app.Resources.MergedDictionaries.Add(themeDict);
        }

        public static string GetCurrentTheme()
        {
            return _currentTheme;
        }
    }
}
