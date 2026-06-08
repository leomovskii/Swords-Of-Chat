namespace SwordsOfChat.Bot.Commands {
	internal class LicenseBotCommand : IBotCommand {

		public string Key => "license";

		public string? Run(long _, string[] __) {
			return BotConfig.GetLicenseText();
		}
	}
}