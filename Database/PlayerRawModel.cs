
namespace SwordsOfChat.Database {
	internal class PlayerRawModel {
		public long UserId { get; set; }
		public string Username { get; set; } = "Player";

		public int Level { get; set; } = 1;
		public int Experience { get; set; } = 0;

		public int CurrentHealth { get; set; }
		public int TotalHealth { get; set; }
		public long HealthTimestamp { get; set; }

		public int CurrentStamina { get; set; }
		public int TotalStamina { get; set; }
		public long StaminaTimestamp { get; set; }

		public int CurrentMovement { get; set; }
		public int TotalMovement { get; set; }
		public long MovementTimestamp { get; set; }

		public int Strength { get; set; }
		public int Endurance { get; set; }
		public int Agility { get; set; }

		public int Money { get; set; }
		public int Gems { get; set; }

		public int Location { get; set; }

		public int Rank { get; set; }

		public int GuildId { get; set; }

	}
}