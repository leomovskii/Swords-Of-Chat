namespace SwordsOfChat.Database {
	internal interface IDatabase {
		bool HasPlayerModel(long userId);
		bool TryGetPlayerModel(long userId, out PlayerModel? model);
		void CreatePlayerModel(long userId);
	}
}