using System.Text.Json;

namespace SwordsOfChat.Localization {
	internal static class LocaleLoader {

		private const string ConfigPath = "../../../Resources/Localization/{0}/config.json";
		private const string LocaleDirPath = "../../../Resources/Localization/{0}/";
		private const string LocaleFileExtension = "*.locale";

		public static LocaleData? TryGetLocaleData(Locale locale) {
			LocaleData? localeData = TryLoadConfig(locale);
			if (localeData == null)
				return null;

			string[] files = GetLocalizationFilesPaths(locale);
			if (files.Length == 0)
				return null;

			for (int i = 0; i < files.Length; i++) {
				var loadedData = TryParseLocaleFile(locale, files[i]);
				if (loadedData == null)
					continue;

				foreach (var e in loadedData)
					if (!localeData.Data.TryAdd(e.Key, e.Value))
						LogError(locale, $"Found duplicate key '{e.Key}' in '{files[i]}' and skipped.");
			}

			return localeData;
		}

		private static LocaleData? TryLoadConfig(Locale locale) {
			string path = string.Format(ConfigPath, locale);

			if (!File.Exists(path)) {
				LogFatal(locale, "Config file not found.");
				return null;
			}

			if (!Utils.IsFileReadable(path)) {
				LogFatal(locale, "Config file is not readable.");
				return null;
			}

			string configJson;
			try {
				configJson = File.ReadAllText(path);
				if (string.IsNullOrEmpty(configJson)) {
					LogFatal(locale, "Config file is empty.");
					return null;
				}

			} catch (Exception ex) {
				LogFatal(locale, ex.Message);
				return null;
			}

			LocaleData? data;
			try {
				data = JsonSerializer.Deserialize<LocaleData>(configJson, BotConfig.JSO);
				if (data == null) {
					LogFatal(locale, "Can't parse config file.");
					return null;
				}

			} catch (Exception ex) {
				LogFatal(locale, $"Can't parse config file. {ex.Message}");
				return null;
			}

			if (!data.Include) {
				LogFatal(locale, $"Config not included.");
				return null;
			}

			if (string.IsNullOrWhiteSpace(data.Emoji)) {
				LogFatal(locale, $"Config missing '{data.Emoji}' field.");
				return null;
			}

			if (data.Ietf == null || data.Ietf.Length == 0 || string.IsNullOrWhiteSpace(data.Ietf[0])) {
				LogFatal(locale, $"Config missing '{data.Ietf}' field.");
				return null;
			}

			return data;
		}

		private static string[] GetLocalizationFilesPaths(Locale locale) {
			try {
				string path = string.Format(LocaleDirPath, locale);
				string[] files = Directory.GetFiles(path, LocaleFileExtension, SearchOption.AllDirectories);
				if (files.Length == 0)
					LogFatal(locale, "Locale files not found.");
				return files;

			} catch (Exception ex) {
				LogFatal(locale, $"Unable to scan locale directory. {ex.Message}");
				return [];
			}
		}

		private static Dictionary<string, string>? TryParseLocaleFile(Locale locale, string path) {
			if (!Utils.IsFileReadable(path)) {
				LogError(locale, $"Locale file '{path}' is not readable.");
				return null;
			}

			string content;
			try {
				content = File.ReadAllText(path);
				if (string.IsNullOrEmpty(content)) {
					LogError(locale, $"Locale file '{path}' is empty.");
					return null;
				}

			} catch (Exception ex) {
				LogError(locale, ex.Message);
				return null;
			}

			return ParseLines(locale, content, path, true);
		}

		private static Dictionary<string, string>? ParseLines(Locale locale, string content, string path, bool stopIfError) {
			var entries = new Dictionary<string, string>();
			var lines = content.Split('\n');

			string? currentKey = null;
			var contentLines = new List<string>();
			int openTagCount = 0;
			int closeTagCount = 0;
			bool valid = true;

			for (int i = 0; i < lines.Length; i++) {
				var raw = lines[i];
				var line = raw.TrimEnd('\r');

				if (TryParseOpenTag(line, out var key)) {
					openTagCount++;

					if (currentKey is not null) {
						LogError(locale, $"'{path}' line {i + 1}: opening tag '<<{key}>>' inside unclosed '<<{currentKey}>>'.");
						valid = false;

						if (stopIfError)
							return null;
						else
							continue;
					}

					currentKey = key;
					contentLines = [];
				} else if (TryParseCloseTag(line, out var closeKey)) {
					closeTagCount++;

					if (currentKey is null) {
						LogError(locale, $"'{path}' line {i + 1}: closing tag '<</{closeKey}>>' without matching opening tag.");
						valid = false;

						if (stopIfError)
							return null;
						else
							continue;
					}

					if (closeKey != currentKey) {
						LogError(locale, $"'{path}' line {i + 1}: closing tag '<</{closeKey}>>' does not match opening '<<{currentKey}>>'.");

						currentKey = null;
						if (stopIfError)
							return null;
						else
							continue;
					}

					var value = string.Join("\n", contentLines).Trim();

					if (!entries.TryAdd(currentKey, value))
						LogError(locale, $"'{path}' line {i + 1}: duplicate key '{currentKey}' — skipping.");

					currentKey = null;

				} else {
					if (currentKey != null)
						contentLines.Add(line);

					// else if (!string.IsNullOrWhiteSpace(line))
					// ignore text outside tags
				}
			}

			// Unclosed tag at EOF?
			if (!string.IsNullOrEmpty(currentKey)) {
				LogError(locale, $"'{path}': tag '<<{currentKey}>>' was never closed.");
				valid = false;
				return null;
			}

			// Unbalanced counts (extra safeguard)?
			if (openTagCount != closeTagCount) {
				LogError(locale, $"'{path}': tag count mismatch — {openTagCount} opening vs {closeTagCount} closing tags.");
				valid = false;
				return null;
			}

			return valid ? entries : null;
		}

		// <<key>>  — the key is a single line, any characters except newline
		static bool TryParseOpenTag(string line, out string key) {
			var trimmed = line.Trim();
			if (trimmed.StartsWith("<<") && trimmed.EndsWith(">>") && !trimmed.StartsWith("<</")) {
				key = trimmed[2..^2]; // strip << and >>
				return key.Length > 0;
			}
			key = string.Empty;
			return false;
		}

		private static bool TryParseCloseTag(string line, out string key) {
			var trimmed = line.Trim();
			if (trimmed.StartsWith("<</") && trimmed.EndsWith(">>")) {
				key = trimmed[3..^2]; // strip <</ and >>
				return key.Length > 0;
			}
			key = string.Empty;
			return false;
		}

		private static void LogFatal(Locale locale, string message) {
			Log.Error($"Failed to load locale {locale}: {message}");
		}

		private static void LogError(Locale locale, string message) {
			Log.Error($"An error occured while loading locale {locale}: {message}");
		}
	}
}