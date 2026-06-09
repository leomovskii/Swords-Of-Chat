using SwordsOfChat.Database;

namespace SwordsOfChat.Game {
	internal static class GameHelper {
		public static int GetExpToLevel(int targetLevel) {
			return 10 * Math.Max(targetLevel, 2);
		}

		internal static (float currentCargo, float totalCargo) CalculateCargo(PlayerModel p) {
			return (0f, 0f);
		}

		internal static string GetGuildName(int guildId) {
			return "❓ Untitled";
		}

		public static (int x, int y) LocationToCoords(int location) {
			return (location % GameConstants.WorldWidth, location / GameConstants.WorldWidth);
		}

		public static int CoordsToLocation((int x, int y) coords) {
			return coords.x + coords.y * GameConstants.WorldWidth;
		}

		internal static string GetLocationTitle((int x, int y) coords) {
			return $"Untitled, {coords.x + 1}x{coords.y + 1}";
		}

		public static string GetKarmaName(int level) {
			if (level < 1)
				return "Cursed";

			if (level < 50)
				return "Extremely Low";

			if (level < 100)
				return "Very Low";

			if (level < 200)
				return "Low";

			if (level < 400)
				return "Decreased";

			if (level < 700)
				return "Normal";

			if (level < 800)
				return "Good";

			if (level < 900)
				return "Enhanced";

			if (level < 950)
				return "High";

			if (level < 999)
				return "Very High";

			return "Divine";
		}
	}
}