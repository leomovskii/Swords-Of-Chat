using System.Text.Json;
using System.Text.RegularExpressions;

namespace SwordsOfChat {
	internal static partial class EmojiHelper {

		private const string FilePath = "../../../Resources/emoji.json";

		private static Dictionary<string, string> All = [];

		private static bool _loading;

		public static bool LoadEmojis() {
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

			Dictionary<string, string>? data;
			try {
				data = JsonSerializer.Deserialize<Dictionary<string, string>>(content, BotConfig.JSO);
				if (data == null) {
					LogError("File is not json.");
					return _loading = false;
				}

			} catch (Exception ex) {
				LogError(ex.Message);
				return _loading = false;
			}

			All = data;
			_loading = false;
			Log.Info($"Loaded {data.Count} emojis.");

			return true;

			static void LogError(string text) {
				Log.Error($"Error while loading emoji mapper file: {text}");
			}
		}

		[GeneratedRegex(@"<emoji:([^>]+)>")]
		public static partial Regex EmojiTagRegex();

		public static string? WorkOn(string? text) {
			return string.IsNullOrEmpty(text) ? text :
				EmojiTagRegex().Replace(text, match => {
					var name = match.Groups[1].Value;
					if (!All.TryGetValue(name, out string? emoji))
						Log.Warning($"Emoji not find for '{name}' key.");
					return emoji ?? string.Empty;
				});
		}
	}
}