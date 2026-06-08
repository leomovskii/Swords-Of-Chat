using SwordsOfChat.Bot;

namespace SwordsOfChat.Commands {
	internal class StartCommand : ICommand {

		public string Key => "start";
		public string[] Aliases => [];
		public string Help => "Start bot.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			TelegramBot.Start();
		}
	}
}