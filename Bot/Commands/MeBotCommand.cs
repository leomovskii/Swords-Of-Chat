using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class MeBotCommand : IBotCommand {

		public string Key => "me";
		public string[] Aliases => [];

		public string? Run(long userId, string[] _) {
			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			string rawText = LocalesManager.Localize(p.Locale, Key, string.Empty);

			bool hasGuild = DBController.Instance.TryGetGuild(p.GuildTag, out Guild guild);
			var e0 = (hasGuild ? $"[{guild.Tag}] " : string.Empty) + p.Username;

			var e1 = p.Karma;
			var e2 = GameHelper.GetKarmaName(p.Karma);

			var e3 = p.Experience;

			var e4 = p.Health.Value;
			var e5 = p.Health.Total;

			(var e6, var e7, var e8) = p.Vigor.GetFormatEntries();
			(var e9, var e10, var e11) = p.Movement.GetFormatEntries();

			var e12 = p.Money;
			var e13 = p.Gems;

			var e14 = GameHelper.GetLocationTitle(p.Location);

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);
			var e15 = currentCargo.ToString("0.##");
			var e16 = totalCargo.ToString("0.##");

			var e17 = GameConfig.GetCurrentRank(p.Prestige);
			var e18 = guild.GetFullTitle();

			return string.Format(rawText,
				e0, e1, e2, e3, e4, e5, e6, e7, e8, e9,
				e10, e11, e12, e13, e14, e15, e16, e17, e18);
		}
	}
}