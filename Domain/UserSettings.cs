using System.Configuration;

namespace TODO.Domain;

internal class UserSettings : ConfigurationSection
{
    [ConfigurationProperty("Theme", DefaultValue = Themes.DarkTheme)]
    public Themes Theme
    {
        get => (Themes)this[nameof(Theme)];
        set => this[nameof(Theme)] = value;
    }
}