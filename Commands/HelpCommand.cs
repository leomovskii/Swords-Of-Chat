namespace TgGame.Commands {
	internal class HelpCommand : ICommand {

		public string Key => "help";
		public string[] Aliases => [];
		public string Help => "Show all commands.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			if (args.Length == 1) {
				if (Program.CommandsHelp.Length == 0)
					Log.Info("No available commands.");
				else
					Log.Info($"Available commands:\n{Program.CommandsHelp}");
			}

			string cmd = args[1].ToLower();
			if (!Program.Commands.TryGetValue(cmd, out var command)) {
				Log.Error($"Command {cmd} not found.");
				return;
			}

			Log.Info(command.DetailedHelp ?? command.Help);
		}
	}
}