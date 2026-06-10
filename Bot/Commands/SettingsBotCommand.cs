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

			string rawText = LocalesManager.Localize(p.Locale, Key, string.Empty);

			var tag = LocalesManager.GetIetfTag(p.Locale);

			var e0 = $"<emoji:lang_{tag}>{p.Locale}";

			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < LocalesManager.Available.Count; i++) {
				Locale l = LocalesManager.Available[i];
				if (l == p.Locale)
					continue;

				string ls = l.ToString();
				if (sb.Length > 0)
					sb.Append('\n');

				tag = LocalesManager.GetIetfTag(p.Locale);
				sb.Append($"Change to <emoji:lang_{tag}>{ls} /{LangBotCommand.Key0}_{ls.ToLower()}");
			}
			var e1 = sb.ToString();

			return string.Format(rawText, e0, e1);
		}
	}
}