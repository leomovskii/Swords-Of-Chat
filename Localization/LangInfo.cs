namespace SwordsOfChat.Localization {
	internal struct LangInfo(Lang lang, string emoji, params string[] ietfCodes) {
		public Lang Lang { get; private set; } = lang;
		public string Emoji { get; private set; } = emoji;
		public string[] IetfCodes { get; private set; } = ietfCodes;
	}
}