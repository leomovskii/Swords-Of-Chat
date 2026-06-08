namespace SwordsOfChat.Database {
	internal static class DBController {

		private static IDatabase? _instance;
		public static IDatabase Instance => _instance ??= GetNewDefaultInstance();

		public static void Init() {
			if (_instance != null)
				return;

			_instance = GetNewDefaultInstance();
			Log.Info("Database connected.");
		}

		public static IDatabase GetNewDefaultInstance() {
			return new RAMDatabase();
		}
	}
}