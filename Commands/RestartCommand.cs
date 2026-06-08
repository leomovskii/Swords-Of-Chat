using TgGame.Bot;

namespace TgGame.Commands {
	internal class RestartCommand : ICommand {

		public string Key => "restart";
		public string[] Aliases => [];
		public string Help => "Restart bot.";
		public string? DetailedHelp => Help;

		public void Run(params string[] args) {
			TelegramBot.Restart();
		}
	}
}