namespace SwordsOfChat.Commands {
	internal interface ICommand {
		string Key { get; }
		string[] Aliases { get; }
		string Help { get; }
		string? DetailedHelp { get; }
		void Run(params string[] args);
	}
}