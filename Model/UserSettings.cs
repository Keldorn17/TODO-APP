using System.ComponentModel;
using System.Configuration;

namespace TODO.Model
{
    class UserSettings:ConfigurationSection
    {

		[ConfigurationProperty("Theme", DefaultValue = "DarkTheme")]
		public string Theme
		{
			get { return (string)this["Theme"]; }
			set { this["Theme"] = value; }
		}


	}
}
