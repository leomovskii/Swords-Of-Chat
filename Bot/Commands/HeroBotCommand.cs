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
			var e2 = GameHelper.GetKarmaName(p.Karma);

			var e3 = p.Experience;

			var e4 = p.Health.Value;
			var e5 = p.Health.Total;

			(var e6, var e7, var e8) = p.Vigor.GetFormatEntries();
			(var e9, var e10, var e11) = p.Movement.GetFormatEntries();

			var e12 = p.Money;
			var e13 = p.Gems;

			var e14 = GameHelper.GetLocationTitle(p.Location);

			var e15 = p.Strength;
			var e16 = p.Endurance;
			var e17 = p.Agility;

			var e18 = p.AttackDamage;
			var e19 = p.AttackSpeed;
			var e20 = p.Armor;
			var e21 = p.Lethality;
			var e22 = p.CritChance;
			var e23 = p.CritDamage;
			var e24 = p.Accuracy;
			var e25 = p.Evasion;
			var e26 = p.Tenacity;

			(float currentCargo, float totalCargo) = GameHelper.CalculateCargo(p);
			var e27 = currentCargo.ToString("0.##");
			var e28 = totalCargo.ToString("0.##");

			var e29 = PurchasingManager.GetCurrentRank(p.Prestige);
			var e30 = guild.GetFullTitle();

			return string.Format(rawText,
				e0, e1, e2, e3, e4, e5, e6, e7, e8, e9,
				e10, e11, e12, e13, e14, e15, e16, e17, e18, e19,
				e20, e21, e22, e23, e24, e25, e26, e27, e28, e29,
				e30);
		}
	}
}