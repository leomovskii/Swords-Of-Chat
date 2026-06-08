using SwordsOfChat.Database;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class SettingsBotCommand : IBotCommand {

		public const string Key0 = "settings";

		public string Key => Key0;

		public string? Run(long userId, string[] _) {
			if (!ResourcesHelper.TryGetText(Key, out string rawText))
				return null;

			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			Lang currentLang = p.Language == Lang.Unset ? Lang.English : p.Language;
			var e0 = $"{LangManager.GetEmojiFlag(currentLang)}{currentLang}";

			var sb = new System.Text.StringBuilder();
			for (int i = 0; i < LangManager.All.Length; i++) {
				Lang l = LangManager.All[i];
				if (l == currentLang)
					continue;

				string ls = l.ToString();
				if (sb.Length > 0)
					sb.Append('\n');

				sb.Append($"Change to {LangManager.GetEmojiFlag(l)}{ls} /{LangBotCommand.Key0}_{ls.ToLower()}");
			}
			var e1 = sb.ToString();

			return string.Format(rawText, e0,e1);
		}
	}
}