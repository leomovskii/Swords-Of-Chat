namespace SwordsOfChat.Bot.Commands {
	internal class TODOInventoryBotCommand : IBotCommand {

		public string Key => "inventory";
		public string[] Aliases => ["equipment"];

		public string? Run(long userId, string[] _) {
			return null; // todo
		}
	}
}