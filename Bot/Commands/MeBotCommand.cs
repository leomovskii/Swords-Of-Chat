using SwordsOfChat.Database;
using SwordsOfChat.Game;

namespace SwordsOfChat.Bot.Commands {
	internal class MeBotCommand : IBotCommand {

		public string Key => "me";
		public bool KeyIsPrefix => false;

		public string? Run(long userId, string _) {
			if (!ResourcesHelper.TryGetText(Key, out string rawText))
				return null;

			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			var e0 = p.Username;

			int expTarget = GameHelper.GetExpToLevel(p.Level + 1);
			var e1 = p.Level;
			var e2 = p.Experience;
			var e3 = expTarget;
			var e4 = ((float) p.Experience / expTarget).ToString("0.##");

			var e5 = p.Health.Value;
			var e6 = p.Health.Total;

			var e7 = p.Stamina.Value;
			var e8 = p.Stamina.Total;
			var e9 = p.Stamina.Value >= p.Stamina.Total ? string.Empty : $"(next in {Utils.GetTimeText(p.Stamina.GetNextTime())})";

			var e10 = p.Movement.Value;
			var e11 = p.Movement.Total;
			var e12 = p.Movement.Value >= p.Movement.Total ? string.Empty : $"(next in {Utils.GetTimeText(p.Movement.GetNextTime())})";

			var e13 = p.Money;
			var e14 = p.Gems;

			var e15 = GameHelper.GetLocationTitle(p.Location);

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);

			var e16 = currentCargo.ToString("0.##");
			var e17 = totalCargo.ToString("0.##");

			var e18 = p.Rank;
			var e19 = p.GuildId < 0 ? "❓ No Guild" : GameHelper.GetGuildName(p.GuildId);

			return string.Format(rawText, e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19);
		}
	}
}