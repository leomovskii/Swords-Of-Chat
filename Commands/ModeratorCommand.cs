namespace SwordsOfChat.Commands {
	internal class ModeratorCommand : ICommand {
		public string Key => "moderator";
		public string[] Aliases => ["mod", "moder"];
		public string Help => "Managing the moderators list.";

		private readonly string _detailedHelp;
		public string? DetailedHelp => _detailedHelp;

		public ModeratorCommand() {
			var sb = new System.Text.StringBuilder();

			sb.Append($"Command help: {Key}");
			if (Aliases.Length > 0)
				sb.Append($"\n Aliaces: {string.Join(", ", Aliases)}");
			sb.Append("\n Usages:");
			sb.Append($"\n  {Key} list - list of all moderators");
			sb.Append($"\n  {Key} add $ID - add user to moderators list");
			sb.Append($"\n  {Key} remove $ID - remove user from moderators list");
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
					"add" => UserLevel.Moderator,
					_ => UserLevel.User
				};

				BotConfig.SetUserLevel(userId, level);

			} else if (subCmd == "list")
				Log.Info(BotConfig.GetUsersList(UserLevel.Moderator));

			else
				Log.Info(DetailedHelp);
		}
	}
}
