namespace TgGame.BotCommands {
	internal interface IBotCommand {
		string Key { get; }
		string? Run(long userId);
	}
}