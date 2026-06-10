using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class RulesBotCommand : IBotCommand {

		public string Key => "rules";
		public string[] Aliases => ["terms"];

		public string? Run(long userId, string[] _) {
			bool hasPlayer = DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) && p != null;

			string rawText = LocalesManager.Localize(p.Locale, Key, string.Empty);

			int karma = hasPlayer ? p!.Karma : 0;
			var e0 = $"\n<emoji:karma> Karma: {karma} ({GameHelper.GetKarmaName(karma)}) /karma\n";

			return string.Format(rawText, e0);
		}
	}
}