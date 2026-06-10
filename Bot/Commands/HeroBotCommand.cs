using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Localization;
using SwordsOfChat.Purchasing;

namespace SwordsOfChat.Bot.Commands {
	internal class HeroBotCommand : IBotCommand {

		public string Key => "hero";
		public string[] Aliases => [];

		public string? Run(long userId, string[] _) {
			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			string rawText = LocalesManager.Localize(p.Locale, Key, string.Empty);

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

			var e10 = p.Vigor.Value;
			var e11 = p.Vigor.Total;
			var e12 = p.Vigor.Value >= p.Vigor.Total ? string.Empty : $" (next in {Utils.GetTimeText(p.Vigor.GetNextTime())})";

			var e13 = p.Movement.Value;
			var e14 = p.Movement.Total;
			var e15 = p.Movement.Value >= p.Movement.Total ? string.Empty : $" (next in {Utils.GetTimeText(p.Movement.GetNextTime())})";

			var e16 = p.Money;
			var e17 = p.Gems;

			var e18 = GameHelper.GetLocationTitle(p.Location);

			var e19 = p.Strength;
			var e20 = p.Endurance;
			var e21 = p.Agility;

			var e22 = p.AttackDamage;
			var e23 = p.AttackSpeed;
			var e24 = p.Armor;
			var e25 = p.Lethality;
			var e26 = p.CritChance;
			var e27 = p.CritDamage;
			var e28 = p.Accuracy;
			var e29 = p.Evasion;
			var e30 = p.Tenacity;

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);
			var e31 = currentCargo.ToString("0.##");
			var e32 = totalCargo.ToString("0.##");

			var e33 = PurchasingManager.GetCurrentRank(p.Prestige);
			var e34 = guild.GetFullTitle();

			return string.Format(rawText,
				e0, e1, e2, e3, e4, e5, e6, e7, e8, e9,
				e10, e11, e12, e13, e14, e15, e16, e17, e18, e19,
				e20, e21, e22, e23, e24, e25, e26, e27, e28, e29,
				e30, e31, e32, e33, e34);
		}
	}
}