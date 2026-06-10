using System.Text.Json.Serialization;

namespace SwordsOfChat.Localization {
	internal class LocaleData {
		public bool Include { get; set; } = true;
		public required string Emoji { get; set; }
		public required string[] Ietf { get; set; }
		[JsonIgnore] public Dictionary<string, string> Data { get; set; } = [];
	}
}