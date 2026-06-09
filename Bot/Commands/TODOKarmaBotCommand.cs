namespace SwordsOfChat.Bot.Commands {
	internal class TODOKarmaBotCommand : IBotCommand {

		public string Key => "karma";
		public string[] Aliases => [];

		public string? Run(long userId, string[] _) {
			return null; // todo
		}
	}
}