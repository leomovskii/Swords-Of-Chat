namespace SwordsOfChat.Game.Maps {
	internal static class MapsLoader {

		private const string MapEmojisPath = "../../../Resources/Maps/map_emojis.txt";

		public static Dictionary<string, string> LoadMapEmojis() {
			if (!File.Exists(MapEmojisPath)) {
				LogError("File not found.");
				return [];
			}

			if (!Utils.IsFileReadable(MapEmojisPath)) {
				LogError("File is not readable.");
				return [];
			}

			string content;
			try {
				content = File.ReadAllText(MapEmojisPath);
				if (string.IsNullOrEmpty(content)) {
					LogError("File is empty.");
					return [];
				}

			} catch (Exception ex) {
				LogError(ex.Message);
				return [];
			}

			var lines = content.Trim().Split('\n');
			if (lines.Length == 0) {
				LogError("Wrong structure.");
				return [];
			}

			var loaded = new Dictionary<string, string>();

			for (int i = 0; i < lines.Length; i++) {
				string[] args = lines[i].Split(' ');
				if (args.Length > 1) {
					if (!loaded.TryAdd(args[0], string.Join(' ', args[1..])))
						LogError($"Found emoji duplicate '{args[0]}'.");
				}
			}

			return loaded;

			static void LogError(string text) {
				Log.Error($"Error while loading mapper file: {text}");
			}
		}
	}
}