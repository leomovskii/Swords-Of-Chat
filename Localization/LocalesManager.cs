namespace SwordsOfChat.Localization {
	internal static class LocalesManager {

		public const Locale DefaultLocale = Locale.English;

		public readonly static Locale[] All = Enum.GetValues<Locale>();
		public readonly static List<Locale> Available = [];

		private readonly static Dictionary<Locale, LocaleData> Data = [];

		private static bool _initialized;

		public static bool Valid => HasLocale(DefaultLocale);

		public static void Initialize(params Locale[] toLoadLocales) {
			if (_initialized)
				return;
			_initialized = true;

			Log.Info("Localization init start.");

			var source = toLoadLocales.Length > 0 ? toLoadLocales : All;

			for (int i = 0; i < source.Length; i++) {
				LocaleData? data = LocaleLoader.TryGetLocaleData(source[i]);
				if (data != null && Data.TryAdd(source[i], data))
					Available.Add(source[i]);
			}

			Log.Info($"Localization init finish. Loaded {Data.Count} locales.");
		}

		public static bool HasLocale(Locale locale) {
			return Data.ContainsKey(locale);
		}

		public static bool HasKey(Locale locale, string key) {
			return Data.TryGetValue(locale, out LocaleData? ld) && ld.Data.ContainsKey(key);
		}

		public static string Localize(Locale locale, string key, string defaultValue) {
			if (Data.TryGetValue(locale, out LocaleData? ld))
				if (ld.Data.TryGetValue(key, out string? localised))
					return localised ?? defaultValue;

			return locale != DefaultLocale ? Localize(DefaultLocale, key, defaultValue) : defaultValue;
		}

		public static string? GetIetfTag(Locale locale) {
			return Data.TryGetValue(locale, out var data) && data != null ? data.Ietf.FirstOrDefault() : null;
		}

		public static Locale? IetfToLang(string? ietf) {
			if (string.IsNullOrWhiteSpace(ietf))
				return null;

			foreach (var e in Data)
				if (e.Value.Ietf.Contains(ietf))
					return e.Key;

			return null;
		}
	}
}