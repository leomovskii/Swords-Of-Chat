using SwordsOfChat.Database;
using SwordsOfChat.Game;
using SwordsOfChat.Localization;

namespace SwordsOfChat.Bot.Commands {
	internal class InfoBotCommand : IBotCommand {

		private readonly Dictionary<string, Func<long, string?>> All = [];
		private readonly List<string> Commands = [];

		public string Key => "info";
		public string[] Aliases => [];

		public InfoBotCommand() {
			AddInfoCommand("experience", InfoExperience, ["wisdom", "exp", "wis"]);
			AddInfoCommand("health", InfoHealth, "hp");
			AddInfoCommand("vigor", InfoVigor, "vig");
			AddInfoCommand("movement", InfoMovement, ["mvnt", "move", "mov"]);
			AddInfoCommand("money", InfoMoney, "mon");
			AddInfoCommand("gems", InfoGems, "gem");
			AddInfoCommand("map", InfoMap);
			AddInfoCommand("strength", InfoStrength, "str");
			AddInfoCommand("endurance", InfoEndurance, "end");
			AddInfoCommand("agility", InfoAgility, "agi");
			AddInfoCommand("damage", InfoDamage, ["dmg", "atd"]);
			AddInfoCommand("attack_speed", InfoAttackSpeed, ["attackspeed", "ats"]);
			AddInfoCommand("armor", InfoArmor, ["defence", "arm", "def"]);
			AddInfoCommand("lethality", InfoLethality, "let");
			AddInfoCommand("critical_strike", InfoCriticalStrike, ["criticalstrike", "critical_chance", "criticalchance", "critical_power", "criticalpower", "crit_strike", "critstrike", "crit_chance", "critchance", "crit_power", "critpower", "crit"]);
			AddInfoCommand("accuracy", InfoAccuracy, "acc");
			AddInfoCommand("evasion", InfoEvasion, "eva");
			AddInfoCommand("tenacity", InfoTenacity, "ten");
			AddInfoCommand("guild", InfoGuild, ["clan", "gld", "cln"]);
			AddInfoCommand("prestige", InfoPrestige, "pre");
		}

		private void AddInfoCommand(string command, Func<long, string?> callback, params string[] aliases) {
			if (string.IsNullOrEmpty(command)) {
				Log.Error($"Fail to add info bot command '{command}': Command text is empty.");
				return;
			}

			if (!All.TryAdd(command, callback)) {
				Log.Error($"Fail to add info bot command '{command}': Command already exists.");
				return;
			}

			Commands.Add($"/{Key}_{command}");

			for (int i = 0; i < aliases.Length; i++)
				if (!string.IsNullOrEmpty(aliases[i]))
					if (!All.TryAdd(aliases[i], callback))
						Log.Error($"Fail to add info bot command '{command}' alias: Command already exists.");
		}

		public string? Run(long userId, string[] args) {
			if (args.Length < 2)
				return InfoCommand(userId);

			string key = string.Join('_', args[1..]).ToLower();
			return All.TryGetValue(key, out var func) ? func(userId) : null;
		}

		private string? InfoCommand(long userId) {
			var loc = DBController.Instance.GetPlayerLocale(userId);
			string rawText = LocalesManager.Localize(loc, Key, string.Empty);
			return string.Format(rawText, string.Join('\n', Commands));
		}

		private string? InfoExperience(long userId) {
			if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
				return null;

			string rawText = LocalesManager.Localize(p.Locale, "info_experience", string.Empty);

			var e0 = p.Experience;
			var e1 = GameHelper.GetWisdomFor(p.Karma);

			return string.Format(rawText, e0, e1);
		}

		private string? InfoHealth(long userId) {
			return null; // todo
		}

		private string? InfoVigor(long userId) {
			return null; // todo
		}

		private string? InfoMovement(long userId) {
			return null; // todo
		}

		private string? InfoMoney(long userId) {
			return null; // todo
		}

		private string? InfoGems(long userId) {
			return null; // todo
		}

		private string? InfoMap(long userId) {
			return null; // todo
		}

		private string? InfoStrength(long userId) {
			return null; // todo
		}

		private string? InfoEndurance(long userId) {
			return null; // todo
		}

		private string? InfoAgility(long userId) {
			return null; // todo
		}

		private string? InfoDamage(long userId) {
			return null; // todo
		}

		private string? InfoAttackSpeed(long userId) {
			return null; // todo
		}

		private string? InfoArmor(long userId) {
			return null; // todo
		}

		private string? InfoLethality(long userId) {
			return null; // todo
		}

		private string? InfoCriticalStrike(long userId) {
			return null; // todo
		}

		private string? InfoAccuracy(long userId) {
			return null; // todo
		}

		private string? InfoEvasion(long userId) {
			return null; // todo
		}

		private string? InfoTenacity(long userId) {
			return null; // todo
		}

		private string? InfoGuild(long userId) {
			return null; // todo
		}

		private string? InfoPrestige(long userId) {
			return null; // todo
		}
	}
}