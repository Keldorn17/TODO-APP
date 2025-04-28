using System.Windows;
using System.Configuration;
using TODO.Domain;

namespace TODO.Utils;
public static class ThemeManager
{
    private static Themes _currentTheme = Themes.DarkTheme;

    private static readonly Configuration UserConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    private const string UserSettings = "UserSettings";

    public static void ToggleTheme()
    {
        var newTheme = _currentTheme == Themes.DarkTheme ? Themes.LightTheme : Themes.DarkTheme;
        ApplyTheme(newTheme);
        _currentTheme = newTheme;
    }

    private static void ApplyTheme(Themes newTheme)
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

        var themeUri = new Uri($"/Themes/ColorThemes/{newTheme.ToString()}.xaml", UriKind.Relative);
        var themeDict = new ResourceDictionary { Source = themeUri };
        app.Resources.MergedDictionaries.Add(themeDict);
    }

    public static Themes GetCurrentTheme()
    {
        return _currentTheme;
    }

    public static void SaveTheme()
    {
        if (UserConfig.GetSection("UserSettings") is not UserSettings userSettingsSection) return;
        userSettingsSection.Theme = _currentTheme;
        UserConfig.Save();
    }

    public static void LoadTheme()
    {        
        if (UserConfig.Sections[UserSettings] is null)
        {
            UserConfig.Sections.Add(UserSettings, new UserSettings());
        }

        if (UserConfig.GetSection("UserSettings") is UserSettings userSettingsSection && _currentTheme != userSettingsSection.Theme)
        {
            ToggleTheme();
        }
        
    }
}
