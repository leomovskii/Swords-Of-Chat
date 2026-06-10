using SwordsOfChat.Localization;

namespace SwordsOfChat.Database {
	internal interface IDatabase {
		bool HasPlayerModel(long userId);
		bool TryGetPlayerModel(long userId, out PlayerModel? model);
		PlayerModel? CreatePlayerModel(long userId);
		Locale GetPlayerLocale(long userId);

		bool HasGuild(string tag);
		bool TryGetGuild(string tag, out Guild guild);
		Guild CreateGuild(string title, string tag, string avatar);
	}
}