namespace SwordsOfChat.Database {
	internal struct Guild(string title, string tag, string avatar) {

		public static readonly Guild NoGuild = new Guild("No guild", string.Empty, "❓");

		public string Title { get; private set; } = title;
		public string Tag { get; private set; } = tag;
		public string Avatar { get; private set; } = avatar;

		public readonly bool IsExistingGuild() {
			return !string.IsNullOrEmpty(Tag);
		}

		public readonly string GetFullTitle() {
			return $"{Avatar} {Title}";
		}
	}
}