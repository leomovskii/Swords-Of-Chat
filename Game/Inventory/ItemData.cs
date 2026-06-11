namespace SwordsOfChat.Game.Inventory {
	internal struct ItemData {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public float Weight { get; set; }
		public Dictionary<string, int> Effects { get; set; }
	}
}