namespace SwordsOfChat.Localization {
	internal static class LangManager {

		public const Lang DefaultLang = Lang.English;

		private readonly static LangInfo[] Langs = [
			new LangInfo(Lang.Czech, "🇨🇿", "cs"),
			new LangInfo(Lang.Danish, "🇩🇰", "da"),
			new LangInfo(Lang.Dutch, "🇳🇱", "nl"),
			new LangInfo(Lang.English, "🇬🇧", "en"), // default
			new LangInfo(Lang.Estonian, "🇪🇪", "et"),
			new LangInfo(Lang.Finnish, "🇫🇮", "fi"),
			new LangInfo(Lang.French, "🇫🇷", "fr"),
			new LangInfo(Lang.German, "🇩🇪", "de"),
			new LangInfo(Lang.Italian, "🇮🇹", "it"),
			new LangInfo(Lang.Japanese, "🇯🇵", "ja"),
			new LangInfo(Lang.Latvian, "🇱🇻", "lv"),
			new LangInfo(Lang.Lithuanian, "🇱🇹", "lt"),
			new LangInfo(Lang.Norwegian, "🇳🇴", ["no", "nb", "nn"]),
			new LangInfo(Lang.Portuguese, "🇵🇹", "pt"),
			new LangInfo(Lang.Romanian, "🇷🇴", "ro"),
			new LangInfo(Lang.Slovenian, "🇸🇮", "sl"),
			new LangInfo(Lang.Spanish, "🇪🇸", "es"),
			new LangInfo(Lang.Swedish, "🇸🇪", "sv"),
			new LangInfo(Lang.Ukrainian, "🇺🇦", "uk")
		];

		public static readonly Lang[] All = Enum.GetValues<Lang>()[1..];

		public static string GetEmojiFlag(Lang lang) {
			if (lang == Lang.Unset)
				lang = DefaultLang;
			return Langs[(int) lang - 1].Emoji;
		}

		public static Lang IetfToLang(string? ietf) {
			if (string.IsNullOrWhiteSpace(ietf))
				return Lang.English;

			string code = ietf.Split('-')[0];
			for (int i = 0; i < Langs.Length; i++)
				if (Langs[i].IetfCodes.Contains(code))
					return Langs[i].Lang;
			return DefaultLang;
		}
	}
}