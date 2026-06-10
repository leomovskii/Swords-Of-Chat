using SwordsOfChat.Game;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Database {
	internal class PlayerRawModel {
		public long UserId { get; set; }
		public string Username { get; set; } = "Player";
		public Locale Locale { get; set; } = LocalesManager.DefaultLocale;

		public int Karma { get; set; } = GameConfig.Instance.InitialKarma;
		public int Experience { get; set; } = 0;

		public int CurrentHealth { get; set; }
		public int TotalHealth { get; set; }
		public long HealthTimestamp { get; set; }

		public int CurrentVigor { get; set; }
		public int TotalVigor { get; set; }
		public long VigorTimestamp { get; set; }

		public int CurrentMovement { get; set; }
		public int TotalMovement { get; set; }
		public long MovementTimestamp { get; set; }

		public int Strength { get; set; }
		public int Endurance { get; set; }
		public int Agility { get; set; }

		public int Money { get; set; }
		public int Gems { get; set; }

		public int Location { get; set; }

		public int Prestige { get; set; }

		public string GuildTag { get; set; } = string.Empty;

	}
}