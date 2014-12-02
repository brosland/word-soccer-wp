using System;
using Windows.Storage;

namespace WordSoccer
{
	public class Settings
	{
		public const String PLAYER_NAME_KEY = "player_name";
		public const String AIPLAYER_NAME_KEY = "aiplayer_name";
		public const String AIPLAYER_LEVEL_KEY = "aiplayer_level";
		public const String DEFAULT_PLAYER_NAME = "You";
		public const String DEFAULT_AIPLAYER_NAME = "AI Johnny";
		public const String DEFAULT_AIPLAYER_LEVEL = "0";

		private readonly ApplicationDataContainer localSettings;

		public Settings(ApplicationDataContainer localSettings)
		{
			this.localSettings = localSettings;
		}

		public Object Load(String key, Object defaultValue)
		{
			if (!localSettings.Values.ContainsKey(key))
			{
				Save(key, defaultValue);
			}

			return localSettings.Values[key];
		}

		public void Save(String key, Object value)
		{
			if (value == null)
			{
				if (localSettings.Values.ContainsKey(key))
				{
					localSettings.Values.Remove(key);
				}
			}
			else
			{
				localSettings.Values[key] = value;
			}
		}
	}
}