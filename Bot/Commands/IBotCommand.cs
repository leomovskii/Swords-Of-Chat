namespace SwordsOfChat.Bot.Commands {
	internal interface IBotCommand {
		string Key { get; }
		bool KeyIsPrefix { get; }
		string? Run(long userId, string command);
	}
}