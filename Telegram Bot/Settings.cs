﻿using System.IO;
using Newtonsoft.Json;

namespace Telegram_Bot
{
	public class Settings
	{
		private static readonly object LoadLock;

		public static Settings Instance { get; private set; }

		private const string SETTINGSFILE = "TelegramSettings.json";

		#region Properties

		public string BotToken { get; set; }

		#endregion Properties

		static Settings ( )
		{
			LoadLock = new object ( );
			Instance = new Settings ( );
			Load ( );
			Save ( );
		}

		public static void Save ( )
		{
			lock ( LoadLock )
				File.WriteAllText ( SETTINGSFILE, JsonConvert.SerializeObject ( Instance, Formatting.Indented ) );
		}

		public static void Load ( )
		{
			lock ( LoadLock )
			{
				if ( File.Exists ( SETTINGSFILE ) )
					Instance = JsonConvert.DeserializeObject<Settings> ( File.ReadAllText ( SETTINGSFILE ) );
			}
		}
	}
}