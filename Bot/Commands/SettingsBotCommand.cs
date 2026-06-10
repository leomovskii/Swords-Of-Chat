using SwordsOfChat.Database;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class SettingsBotCommand : IBotCommand {

		public const string Key0 = "settings";

		public string Key => Key0;
		public string[] Aliases => ["options"];

		public string? Run(long userId, string[] _) {
			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			string rawText = LocalesManager.Localize(p.Language, Key, string.Empty);

			Locale currentLang = p.Language == Locale.Unset ? Locale.English : p.Language;
			var e0 = $"{LocalesManager.GetEmojiFlag(currentLang)}{currentLang}";

			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < LocalesManager.Available.Count; i++) {
				Locale l = LocalesManager.Available[i];
				if (l == currentLang)
					continue;

				string ls = l.ToString();
				if (sb.Length > 0)
					sb.Append('\n');

				sb.Append($"Change to {LocalesManager.GetEmojiFlag(l)}{ls} /{LangBotCommand.Key0}_{ls.ToLower()}");
			}
			var e1 = sb.ToString();

			return string.Format(rawText, e0, e1);
		}
	}
}