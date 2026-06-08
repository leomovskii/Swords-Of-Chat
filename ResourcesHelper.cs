namespace TgGame {
	internal static class ResourcesHelper {
		#region Text

		private readonly static Dictionary<string, string> Texts = [];

		public static bool TryGetText(string text, out string content) {
			string key = text.ToLower().Trim();
			if (string.IsNullOrEmpty(key)) {
				content = string.Empty;
				return false;
			}

			if (Texts.TryGetValue(key, out string? value) && value != null) {
				content = value;
				return true;
			}

			try {
				using StreamReader reader = new StreamReader($"../../../Resources/Text/{key}.txt");
				string data = reader.ReadToEnd().Trim();
				Texts.TryAdd(key, data);

				content = data;
				return true;

			} catch (Exception e) {
				Console.WriteLine($"[Error] Failed to load shader source file: {e.Message}");
				content = string.Empty;
				return false;
			}
		}

		#endregion
	}
}