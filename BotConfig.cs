using System.Diagnostics;
using System.Text.Json;

namespace SwordsOfChat {
	internal struct BotRawConfig {
		public string Token { get; set; } = string.Empty;
		public long Owner { get; set; } = -1;
		public long[] Administrators { get; set; } = [];
		public long[] Moderators { get; set; } = [];
		public string License { get; set; } = string.Empty;

		public BotRawConfig() { }

		public readonly bool Validate() {
			return !string.IsNullOrEmpty(Token) && Token.Trim() == Token && !string.IsNullOrEmpty(License);
		}
	}

	internal static class BotConfig {

		private const string FileName = "config.json";

		private static BotRawConfig Config;
		private readonly static Dictionary<long, UserLevel> Users = [];

		public readonly static string WorkDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

		public readonly static JsonSerializerOptions JSO = new JsonSerializerOptions {
			WriteIndented = true,
			PropertyNameCaseInsensitive = true
		};

		public static long GetOwnerId() {
			return Config.Owner;
		}

		public static bool ValidateConfig() {
			return Config.Validate();
		}

		public static bool TryLoad() {
			try {
				if (!File.Exists(WorkDir)) {
					string fileJson = JsonSerializer.Serialize(new BotRawConfig(), JSO);
					File.WriteAllText(WorkDir, fileJson);
					Log.Warning($"Config file created: Open {FileName} and setup bot before next run.");
					return false;
				}

				string json = File.ReadAllText(WorkDir);
				var data = JsonSerializer.Deserialize<BotRawConfig>(json, JSO);
				if (!data.Validate()) {
					Log.Error($"Config reading failed: Setup {FileName} 'Token', 'License' correctly before next run.");
					return false;
				}

				Config = data;
				Users.Clear();

				Users.Add(data.Owner, UserLevel.Owner);

				for (int i = 0; i < data.Moderators.Length; i++)
					Users.Add(data.Moderators[i], UserLevel.Moderator);

				for (int i = 0; i < data.Administrators.Length; i++)
					Users.Add(data.Administrators[i], UserLevel.Administrator);

				Log.Info($"Config loaded.");
				return true;

			} catch (Exception ex) {
				Config = new();
				Log.Error($"Config deserialize error: {ex.Message}");
				return false;
			}
		}

		public static void OpenFileLocationAndSelectConfig() {
			try {
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

				if (OperatingSystem.IsWindows()) {
					Process.Start("explorer.exe", $"/select,\"{path}\"");
				} else if (OperatingSystem.IsLinux()) {
					Process.Start("xdg-open", Path.GetDirectoryName(path)!);
				} else if (OperatingSystem.IsMacOS()) {
					Process.Start("open", $"-R \"{path}\"");
				}
			} catch (Exception ex) {
				Log.Error($"Failed to open config location: {ex.Message}");
			}
		}

		public static string GetToken() {
			return Config.Token;
		}

		public static UserLevel GetUserLevel(long userId) {
			return Users.TryGetValue(userId, out UserLevel level) ? level : UserLevel.User;
		}

		public static void SetUserLevel(long userId, UserLevel level) {
			if (level == UserLevel.Owner || userId == Config.Owner) {
				Log.Error("Can't change owner status from console.");
				return;
			}

			if (GetUserLevel(userId) == level) {
				Log.Warning($"${userId} already set as {level}.");
				return;
			}

			if (level == UserLevel.User) {
				Users.Remove(userId);
				return;
			}

			if (!Users.TryAdd(userId, level))
				Users[userId] = level;
		}

		public static string GetUsersList(UserLevel level) {
			var found = Users.Where(e => e.Value == level).Select(e => e.Key);
			return found.Any() ? string.Join(' ', found) : "-";
		}

		public static void Save() {
			try {
				Config.Administrators = Users.Where(e => e.Value == UserLevel.Administrator).Select(e => e.Key).ToArray();
				Config.Moderators = Users.Where(e => e.Value == UserLevel.Moderator).Select(e => e.Key).ToArray();

				string json = JsonSerializer.Serialize(Config, JSO);
				File.WriteAllText(WorkDir, json);

				Log.Info("Config saved.");

			} catch (Exception ex) {
				Log.Error($"Config save error: {ex.Message}");
			}
		}

		public static string GetLicenseText() {
			return Config.License;
		}
	}
}