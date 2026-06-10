namespace SwordsOfChat.Game.Maps {
	internal static class MapsManager {

		public readonly static Dictionary<int, GameMap> Map = [];

		private static bool _initialized;

		public static void Init() {
			if (_initialized)
				return;
			_initialized = true;
		}
	}
}