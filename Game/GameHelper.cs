using SwordsOfChat.Database;

namespace SwordsOfChat.Game {
	internal static class GameHelper {

		internal static (float currentCargo, float totalCargo) CalculateCargo(PlayerModel p) {
			return (0f, 0f);
		}

		internal static string GetGuildName(int guildId) {
			return "❓ Untitled";
		}

		#region Karma & Wisdom

		private readonly static int[] KarmaLevels = [100, 200, 500, 700, 800, 900];
		private readonly static int[] WisdomPerKarmaLevel = [30, 60, 85, 100, 105, 110, 120];
		private readonly static string[] KarmaNames = ["Damned", "Terrible", "Bad", "Normal", "Good", "Excellent", "Perfect"];

		public static int GetKarmaLevel(int karma) {
			for (int i = 0; i < KarmaLevels.Length; i++)
				if (karma < KarmaLevels[i])
					return i;

			return KarmaLevels.Length;
		}

		internal static int GetWisdomFor(int karma) {
			return WisdomPerKarmaLevel[GetKarmaLevel(karma)];
		}

		public static string GetKarmaName(int karma) {
			return KarmaNames[GetKarmaLevel(karma)];
		}

		#endregion
		#region Location

		public static (int x, int y) LocationToCoords(int location) {
			return (location % GameConfig.Instance.WorldWidth, location / GameConfig.Instance.WorldWidth);
		}

		public static int CoordsToLocation((int x, int y) coords) {
			return coords.x + coords.y * GameConfig.Instance.WorldWidth;
		}

		internal static string GetLocationTitle((int x, int y) coords) {
			return $"Untitled, {coords.x + 1}x{coords.y + 1}";
		}

		#endregion
	}
}