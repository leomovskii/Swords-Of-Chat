namespace SwordsOfChat.Bot.Commands {
	internal interface IBotCommand {
		string Key { get; }
		string? Run(long userId);
	}
}