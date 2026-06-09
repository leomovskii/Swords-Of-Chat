namespace SwordsOfChat.Bot.Commands {
	internal class TODOMapBotCommand : IBotCommand {

		public string Key => "map";
		public string[] Aliases => ["location"];

		public string? Run(long userId, string[] _) {
			return null; // todo
		}
	}
}