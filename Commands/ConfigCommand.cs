using TgGame.Bot;

namespace TgGame.Commands {
	internal class ConfigCommand : ICommand {

		public string Key => "config";
		public string[] Aliases => ["cfg", "configuration"];
		public string Help => "Configuration file management";

		private readonly string _detailedHelp;
		public string? DetailedHelp => _detailedHelp;

		public ConfigCommand() {
			var sb = new System.Text.StringBuilder();

			sb.Append($"Command help: {Key}");
			if (Aliases.Length > 0)
				sb.Append($"\n Aliaces: {string.Join(", ", Aliases)}");
			sb.Append("\n Usages:");
			sb.Append($"\n  {Key} reload - reload configuration (need bot stop)");
			sb.Append($"\n  {Key} locate - open directory with file in default system browser");
			sb.Append($"\n  {Key} save - modify config with new settings");
			sb.Append($"\n  {Key} help - detailed help for this command");

			_detailedHelp = sb.ToString();
		}

		public void Run(params string[] args) {
			if (args.Length == 1 || args[1].ToLower() == "help") {
				Log.Info(DetailedHelp);
				return;
			}

			switch (args[1].ToLower()) {
				case "reload":
				if (TelegramBot.IsRunning) {
					Log.Error("Can't reload config when bot is running. Stop bot first.");
					return;
				}

				BotConfig.TryLoad();

				break;

				case "locate":
				BotConfig.OpenFileLocationAndSelectConfig();
				break;

				case "save":
				BotConfig.Save();
				break;

				default:
				Log.Info(DetailedHelp);
				break;
			}
		}
	}
}