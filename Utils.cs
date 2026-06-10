namespace SwordsOfChat {
	internal static class Utils {
		public static bool TryParseIDToken(string text, out long userId) {
			string t = text?.TrimStart() ?? "";
			if (t.Length > 0 && t[0] == '$')
				t = t[1..];
			return long.TryParse(t, out userId);
		}

		public static DateTime UnixToUTC(long unix) {
			return DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime;
		}

		public static long UTCToUnix(DateTime dateTime) {
			return new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds();
		}

		public static string GetTimeText(TimeSpan ts) {
			if (ts.TotalSeconds <= 0)
				return "now";

			if (ts.TotalMinutes < 1)
				return "1 min";

			if (ts.TotalHours < 1)
				return $"{Math.Ceiling(ts.TotalMinutes)} min";

			if (ts.TotalDays < 1) {
				int h = (int) Math.Ceiling(ts.TotalHours);
				return h == 1 ? "1 hour" : $"{h} hours";
			}

			int d = (int) Math.Ceiling(ts.TotalDays);
			return d == 1 ? "1 day" : $"{d} days";
		}

		public static bool IsFileReadable(string path) {
			try {
				using var s = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				return s.CanRead;
			} catch {
				return false;
			}
		}
	}
}