using SwordsOfChat.Bot;

namespace SwordsOfChat.Commands {
	internal class StartCommand : ICommand {

		public const string Key0 = "start";

		public string Key => Key0;
		public string[] Aliases => [];
		public string Help => "Start bot.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			TelegramBot.Start();
		}
	}
}