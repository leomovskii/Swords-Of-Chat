
using TgGame.Database;

namespace TgGame.Game {
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
	}
}