namespace TgGame {
	internal enum LogLevel {
		Info, Warning, Error
	}

	internal static class Log {
		public static void Info(string? message) {
			Message(message, LogLevel.Info);
		}

		public static void Warning(string? message) {
			Message(message, LogLevel.Warning);
		}

		public static void Error(string? message) {
			Message(message, LogLevel.Error);
		}

		public static void Message(string? message, LogLevel logLevel) {
			string text = (message ?? "").Trim();
			if (!string.IsNullOrEmpty(text)) {
				var level = logLevel.ToString()[0];
				var timestamp = DateTime.Now.ToString("HH:mm:ss");
				Console.WriteLine($"{level} {timestamp} {text}");
			}
		}
	}
}