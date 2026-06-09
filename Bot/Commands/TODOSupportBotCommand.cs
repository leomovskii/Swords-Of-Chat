namespace SwordsOfChat.Bot.Commands {
	internal class TODOSupportBotCommand : IBotCommand {

		public string Key => "support";
		public string[] Aliases => ["feedback"];

		public string? Run(long userId, string[] _) {
			return null; // todo
		}
	}
}