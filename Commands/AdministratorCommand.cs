namespace TgGame.Commands {
	internal class AdministratorCommand : ICommand {
		public string Key => "administrator";
		public string[] Aliases => ["adm", "admin"];
		public string Help => "Managing the administrators list.";

		private readonly string _detailedHelp;
		public string? DetailedHelp => _detailedHelp;

		public AdministratorCommand() {
			var sb = new System.Text.StringBuilder();

			sb.Append($"Command help: {Key}");
			if (Aliases.Length > 0)
				sb.Append($"\n Aliaces: {string.Join(", ", Aliases)}");
			sb.Append("\n Usages:");
			sb.Append($"\n  {Key} list - list of all administrators");
			sb.Append($"\n  {Key} add $ID - add user to administrators list");
			sb.Append($"\n  {Key} remove $ID - remove user from administrators list");
			sb.Append($"\n  {Key} help - detailed help for this command");

			_detailedHelp = sb.ToString();
		}

		public void Run(params string[] args) {
			if (args.Length == 1 || args[1].ToLower() == "help") {
				Log.Info(DetailedHelp);
				return;
			}

			string subCmd = args[1].ToLower();
			if (subCmd is "add" or "remove") {
				if (args.Length < 3) {
					Log.Warning($"Usage: {Key} {subCmd} $ID");
					return;
				}

				if (!Utils.TryParseIDToken(args[2], out long userId)) {
					Log.Warning($"Failed to parse user id. Input: {args[2]}.");
					return;
				}

				UserLevel level = subCmd switch {
					"add" => UserLevel.Administrator,
					_ => UserLevel.User
				};

				BotConfig.SetUserLevel(userId, level);

			} else if (subCmd == "list")
				Log.Info(BotConfig.GetUsersList(UserLevel.Administrator));

			else
				Log.Info(DetailedHelp);
		}
	}
}
