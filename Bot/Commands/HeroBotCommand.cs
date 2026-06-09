using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Purchasing;

namespace SwordsOfChat.Bot.Commands {
	internal class HeroBotCommand : IBotCommand {

		public string Key => "hero";

		public string? Run(long userId, string[] _) {
			if (!ResourcesHelper.TryGetText(Key, out string rawText))
				return null;

			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			bool hasGuild = DBController.Instance.TryGetGuild(p.GuildTag, out Guild guild);
			var e0 = (hasGuild ? $"[{guild.Tag}] " : string.Empty) + p.Username;

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

			var e16 = p.Strength;
			var e17 = p.Endurance;
			var e18 = p.Agility;

			var e19 = p.AttackDamage;
			var e20 = p.AttackSpeed;
			var e21 = p.Armor;
			var e22 = p.Lethality;
			var e23 = p.CritChance;
			var e24 = p.CritDamage;
			var e25 = p.Accurancy;
			var e26 = p.Evasion;
			var e27 = p.Tenacity;

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);
			var e28 = currentCargo.ToString("0.##");
			var e29 = totalCargo.ToString("0.##");

			var e30 = PurchasingManager.GetCurrentRank(p.Prestige);
			var e31 = guild.GetFullTitle();

			return string.Format(rawText, e0, e1, e2, e3, e4, e5, e6, e7, e8,
				e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20,
				e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31);
		}
	}
}