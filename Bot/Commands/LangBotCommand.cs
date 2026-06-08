using SwordsOfChat.Bot.Commands;
using SwordsOfChat;
using SwordsOfChat.Database;

internal class LangBotCommand : IBotCommand {

	private static readonly Lang[] Languages = Enum.GetValues<Lang>()[1..];

	public string Key => "lang";

	public string? Run(long userId, string[] args) {
		if (args.Length != 2)
			return null;

		if (!DBController.Instance.TryGetPlayerModel(userId, out PlayerModel? p) || p == null)
			return null;

		string input = args[1];

		Lang? found = null;

		for (int i = 0; i < Languages.Length; i++) {
			if (!Languages[i].ToString().StartsWith(input, StringComparison.InvariantCultureIgnoreCase))
				continue;

			if (found != null)
				return null;

			found = Languages[i];
		}

		if (found == null)
			return null;

		p.Language = found.Value;

		return $"Language set to {found}.";
	}
}