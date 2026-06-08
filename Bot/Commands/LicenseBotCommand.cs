namespace SwordsOfChat.Bot.Commands {
	internal class LicenseBotCommand : IBotCommand {

		public string Key => "license";

		public string? Run(long _) {
			return BotConfig.GetLicenseText();
		}
	}
}