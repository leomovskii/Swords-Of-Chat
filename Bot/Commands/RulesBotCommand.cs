using SwordsOfChat.Database;
using SwordsOfChat.Game;

namespace SwordsOfChat.Bot.Commands {
	internal class RulesBotCommand : IBotCommand {

		public string Key => "rules";
		public string[] Aliases => ["terms"];

		public string? Run(long userId, string[] _) {
			if (!ResourcesHelper.TryGetText(Key, out string rawText))
				return null;

			bool hasPlayer = DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) && p != null;
			int karma = hasPlayer ? p!.Karma : 0;

			var e0 = $"\n⚜️ Karma: {karma}/{GameConstants.MaxKarmaLevel} ({GameHelper.GetKarmaName(karma)}) /karma\n";

			return string.Format(rawText, e0);
		}
	}
}