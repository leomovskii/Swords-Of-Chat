namespace SwordsOfChat.Bot.Commands {
	internal class InfoBotCommand : IBotCommand {

		private readonly Dictionary<string, Func<long, string?>> All = [];

		public string Key => "info";

		public InfoBotCommand() {
			AddInfoCommand("experience", InfoExperience, "exp");
			AddInfoCommand("health", InfoHealth, "hp");
			AddInfoCommand("stamina", InfoStamina, ["stam", "sta", "stm"]);
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
			AddInfoCommand("accurancy", InfoAccurancy, "acc");
			AddInfoCommand("evasion", InfoEvasion, "eva");
			AddInfoCommand("tenacity", InfoTenacity, "ten");
			AddInfoCommand("guild", InfoGuild, ["clan", "gld", "cln"]);
			AddInfoCommand("prestige", InfoPrestige, "pre");
		}

		private void AddInfoCommand(string command, Func<long, string?> callback, params string[] aliases) {
			if (string.IsNullOrEmpty(command)) {
				Log.Error($"Unable to add info bot command '{command}'.");
				return;
			}

			if (!All.TryAdd(command, callback))
				Log.Error($"Bot info command '{command}' already exists.");

			for (int i = 0; i < aliases.Length; i++)
				if (!string.IsNullOrEmpty(aliases[i]))
					if (!All.TryAdd(aliases[i], callback))
						Log.Error($"Bot info command alias '{aliases[i]}' already exists.");
		}

		public string? Run(long userId, string[] args) {
			if (args.Length < 2)
				return null;

			string key = string.Join('_', args[1..]).ToLower();
			return All.TryGetValue(key, out var func) ? func(userId) : null;
		}

		private string? InfoExperience(long userId) {
			return null; // todo
		}

		private string? InfoHealth(long userId) {
			return null; // todo
		}

		private string? InfoStamina(long userId) {
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

		private string? InfoAccurancy(long userId) {
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