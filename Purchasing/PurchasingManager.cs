namespace SwordsOfChat.Purchasing {
	internal static class PurchasingManager {
		public readonly static int[] PrestigeToLevel = [
			0, // Commoner
			10, // Patron
			200, // Castellan
			600, // Steward
			1400, // Lord
			2900, // Baron
			5100, // Count
			7000 // Duke
		];

		public static Rank GetCurrentRank(int prestige) {
			for (int i = 0; i < PrestigeToLevel.Length; i++)
				if (prestige >= PrestigeToLevel[i])
					return (Rank) i;
			return Rank.Commoner;
		}
	}
}