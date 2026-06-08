namespace SwordsOfChat.Database {
	internal class Parameter(int value, int total, long timestamp) {

		public int Value { get; private set; } = value;
		public int Total { get; private set; } = total;
		public DateTime Timestamp { get; private set; } = Utils.UnixToUTC(timestamp);

		public event Action? OnChangedEvent;

		public int Waste(int value) {
			return SetValue(Value - value);
		}

		public int Replenish(int value) {
			return SetValue(Value + value);
		}

		public int Reset() {
			return SetValue(0);
		}

		public int Restore() {
			return SetValue(Total);
		}

		public int SetValue(int value) {
			value = Math.Clamp(value, 0, Total);

			if (Value >= Total && value < Total)
				Timestamp = DateTime.UtcNow;

			int delta = Value - value;
			Value = value;
			if (delta != 0)
				OnChangedEvent?.Invoke();
			return delta;
		}

		public TimeSpan GetNextTime() {
			return DateTime.UtcNow - Timestamp;
		}
	}
}