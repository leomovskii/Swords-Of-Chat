namespace SwordsOfChat.Database {
	internal class RAMDatabase : IDatabase {

		private readonly Dictionary<long, PlayerModel> Players = [];

		public RAMDatabase() {
			CreatePlayerModel(BotConfig.GetOwnerId());
		}

		public bool HasPlayerModel(long userId) {
			return Players.ContainsKey(userId);
		}

		public bool TryGetPlayerModel(long userId, out PlayerModel? model) {
			return Players.TryGetValue(userId, out model);
		}

		public void CreatePlayerModel(long userId) {
			if (HasPlayerModel(userId)) {
				Log.Error($"User {userId} already exists.");
				return;
			}

			var m = new PlayerModel(new() { UserId = userId });
			Players.Add(userId, m);
		}
	}
}