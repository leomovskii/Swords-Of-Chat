using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Purchasing;

namespace SwordsOfChat.Bot.Commands {
	internal class MeBotCommand : IBotCommand {

		public string Key => "me";
		public string[] Aliases => [];

		public string? Run(long userId, string[] _) {
			if (!ResourcesHelper.TryGetText(Key, out string rawText))
				return null;

			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			bool hasGuild = DBController.Instance.TryGetGuild(p.GuildTag, out Guild guild);
			var e0 = (hasGuild ? $"[{guild.Tag}] " : string.Empty) + p.Username;

			var e1 = p.Karma;
			var e2 = GameConstants.MaxKarmaLevel;
			var e3 = GameHelper.GetKarmaName(p.Karma);

			int expTarget = GameHelper.GetExpToLevel(p.Level + 1);
			var e4 = p.Level;
			var e5 = p.Experience;
			var e6 = expTarget;
			var e7 = ((float) p.Experience / expTarget).ToString("0.##");

			var e8 = p.Health.Value;
			var e9 = p.Health.Total;

			var e10 = p.Stamina.Value;
			var e11 = p.Stamina.Total;
			var e12 = p.Stamina.Value >= p.Stamina.Total ? string.Empty : $" (next in {Utils.GetTimeText(p.Stamina.GetNextTime())})";

			var e13 = p.Movement.Value;
			var e14 = p.Movement.Total;
			var e15 = p.Movement.Value >= p.Movement.Total ? string.Empty : $" (next in {Utils.GetTimeText(p.Movement.GetNextTime())})";

			var e16 = p.Money;
			var e17 = p.Gems;

			var e18 = GameHelper.GetLocationTitle(p.Location);

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);
			var e19 = currentCargo.ToString("0.##");
			var e20 = totalCargo.ToString("0.##");

			var e21 = PurchasingManager.GetCurrentRank(p.Prestige);
			var e22 = guild.GetFullTitle();

			return string.Format(rawText,
				e0, e1, e2, e3, e4, e5, e6, e7, e8, e9,
				e10, e11, e12, e13, e14, e15, e16, e17, e18, e19,
				e20, e21, e22);
		}
	}
}