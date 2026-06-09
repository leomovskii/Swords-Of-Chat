namespace SwordsOfChat.Bot.Commands {
	internal interface IBotCommand {
		string Key { get; }
		string[] Aliases { get; }
		string? Run(long userId, string[] args);
	}
}