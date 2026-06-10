using System.Text.Json;

namespace SwordsOfChat.Game {
	internal struct GameRawConfig {
		public int[] PrestigeToRankLevels { get; set; } = [10, 200, 600, 1400, 2900, 5100, 7000];
		public int InitialKarma { get; set; } = 700;
		public int MaxKarma { get; set; } = 1000;
		public int VigorRegenTimeSeconds { get; set; } = 3600;
		public int MovementRegenTimeSeconds { get; set; } = 3600;
		public int WorldWidth { get; set; } = 20;
		public int WorldHeight { get; set; } = 20;

		public GameRawConfig() { }
	}

	internal static class GameConfig {

		private const string FilePath = "../../../Resources/game.json";

		public static GameRawConfig Instance { get; private set; }

		private static bool _loading;

		public static bool TryLoad() {
			if (_loading) {
				LogError("Already loading.");
				return false;
			}

			_loading = true;

			if (!File.Exists(FilePath)) {
				LogError("File not found.");
				return _loading = false;
			}

			if (!Utils.IsFileReadable(FilePath)) {
				LogError("File is not readable.");
				return _loading = false;
			}

			string content;
			try {
				content = File.ReadAllText(FilePath);
				if (string.IsNullOrEmpty(content)) {
					LogError("File is empty.");
					return _loading = false;
				}

			} catch (Exception ex) {
				LogError(ex.Message);
				return _loading = false;
			}

			try {
				Instance = JsonSerializer.Deserialize<GameRawConfig>(content, BotConfig.JSO);

				_loading = false;
				Log.Info($"Game config loaded.");
				return true;

			} catch (Exception ex) {
				LogError(ex.Message);
				return _loading = false;
			}

			static void LogError(string text) {
				Log.Error($"Error while initializing game config: {text}");
			}
		}

		public static Rank GetCurrentRank(int prestige) {
			for (int i = 0; i < Instance.PrestigeToRankLevels.Length; i++)
				if (prestige < Instance.PrestigeToRankLevels[i])
					return (Rank) i;
			return Rank.Duke;
		}
	}
}