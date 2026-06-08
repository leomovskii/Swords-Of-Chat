using SwordsOfChat.Database;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class LangBotCommand : IBotCommand {

		public const string Key0 = "lang";

		public string Key => Key0;

		public string? Run(long userId, string[] args) {
			if (args.Length == 1)
				return TelegramBot.TryCallCommandUnsafe(userId, [SettingsBotCommand.Key0]);

			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			string input = args[1];

			Lang? found = null;

			for (int i = 0; i < LangManager.All.Length; i++) {
				if (!LangManager.All[i].ToString().StartsWith(input, StringComparison.InvariantCultureIgnoreCase))
					continue;

				if (found != null)
					return null;

				found = LangManager.All[i];
			}

			if (found == null)
				return null;

			p.Language = found.Value;

			return $"Language set to {found}.";
		}
	}
}