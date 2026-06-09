namespace SwordsOfChat.Database {
	internal class RAMDatabase : IDatabase {

		public RAMDatabase() {
			PlayerModel? m = CreatePlayerModel(BotConfig.GetOwnerId());
			Guild tod = CreateGuild("Tales of Darkness", "TOD", "🎭");
			if (m == null || !tod.IsExistingGuild())
				return;

			m.GuildTag = tod.Tag;
		}

		#region Player behaviour

		private readonly Dictionary<long, PlayerModel> Players = [];

		public bool HasPlayerModel(long userId) {
			return Players.ContainsKey(userId);
		}

		public bool TryGetPlayerModel(long userId, out PlayerModel? model) {
			return Players.TryGetValue(userId, out model);
		}

		public PlayerModel? CreatePlayerModel(long userId) {
			if (!HasPlayerModel(userId)) {
				var m = new PlayerModel(new() { UserId = userId });
				Players.TryAdd(userId, m);
				return m;
			}
			return null;
		}

		#endregion
		#region Guild behaviour

		private readonly Dictionary<string, Guild> Guilds = [];

		public bool HasGuild(string tag) {
			return Guilds.ContainsKey(tag);
		}

		public bool TryGetGuild(string tag, out Guild guild) {
			return (guild = Guilds.TryGetValue(tag, out Guild g) ? g : Guild.NoGuild).IsExistingGuild();
		}

		public Guild CreateGuild(string title, string tag, string avatar) {
			if (!HasGuild(tag)) {
				var g = new Guild(title, tag, avatar);
				Guilds.TryAdd(tag, g);
				return g;
			}
			return Guild.NoGuild;
		}

		#endregion
	}
}