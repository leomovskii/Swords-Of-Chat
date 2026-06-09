namespace SwordsOfChat.Commands {
	internal class ArgsCommand : ICommand {

		public const string StartBotArg = "-sb";
		public const string LocateConfigArg = "-lc";

		public string Key => "args";
		public string[] Aliases => [];
		public string Help => "Get info about args.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			var sb = new System.Text.StringBuilder();

			sb.Append("Args info:");
			sb.Append($"\n  {StartBotArg} : Start bot.");
			sb.Append($"\n  {LocateConfigArg} : Locate config file (open containing folder).");

			Log.Info(sb.ToString());
		}
	}
}