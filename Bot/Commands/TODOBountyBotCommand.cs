namespace SwordsOfChat.Bot.Commands {
	internal class TODOBountyBotCommand : IBotCommand {

		public string Key => "bounty";
		public string[] Aliases => [];

		public string? Run(long userId, string[] _) {
			return null; // todo
		}
	}
}