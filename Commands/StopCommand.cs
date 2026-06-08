using SwordsOfChat.Bot;

namespace SwordsOfChat.Commands {
	internal class StopCommand : ICommand {

		public string Key => "stop";
		public string[] Aliases => [];
		public string Help => "Stop running bot.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			TelegramBot.Stop();
		}
	}
}