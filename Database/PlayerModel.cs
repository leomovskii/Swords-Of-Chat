using TgGame.Game;

namespace TgGame.Database {
	internal class PlayerModel {

		#region Core

		private readonly PlayerRawModel Raw;
		public bool Changed { get; private set; }

		public PlayerModel(PlayerRawModel raw) {
			Raw = raw;

			Health = new(raw.CurrentHealth, raw.TotalHealth, raw.HealthTimestamp);
			Health.OnChangedEvent += SetChanged;

			Stamina = new(raw.CurrentStamina, raw.TotalStamina, raw.StaminaTimestamp);
			Stamina.OnChangedEvent += SetChanged;

			Movement = new(raw.CurrentMovement, raw.TotalMovement, raw.MovementTimestamp);
			Movement.OnChangedEvent += SetChanged;
		}

		public void SetChanged() {
			Changed = true;
		}

		#endregion

		public long UserId => Raw.UserId;

		public string Username {
			get => Raw.Username;
			set {
				Raw.Username = value;
				SetChanged();
			}
		}

		#region Level & Experience 

		public int Level => Raw.Level;
		public int Experience => Raw.Experience;

		public void AddExperience(int expToAdd) {
			if (expToAdd < 1)
				return;

			int lvl = Raw.Level;
			int exp = Raw.Experience + expToAdd;

			int expt = GameHelper.GetExpToLevel(lvl + 1);
			while (exp >= expt) {
				exp -= expt;
				lvl++;
				expt = GameHelper.GetExpToLevel(lvl + 1);
			}

			Raw.Level = lvl;
			Raw.Experience = exp;
			SetChanged();
		}

		public void AddLevels(int levelsCount, bool resetExperience) {
			if (levelsCount < 1)
				return;

			Raw.Level += levelsCount;
			if (resetExperience)
				Raw.Experience = 0;
			SetChanged();
		}

		#endregion
		#region Parameters

		public Parameter Health { get; private set; }
		public Parameter Stamina { get; private set; }
		public Parameter Movement { get; private set; }

		#endregion

		public int Money {
			get => Raw.Money;
			set {
				value = Math.Max(Raw.Money + value, 0);
				if (Raw.Money != value) {
					Raw.Money = value;
					SetChanged();
				}
			}
		}

		public int Gems {
			get => Raw.Gems;
			set {
				value = Math.Max(Raw.Gems + value, 0);
				if (Raw.Gems != value) {
					Raw.Gems = value;
					SetChanged();
				}
			}
		}

		public (int x, int y) Location {
			get => GameHelper.LocationToCoords(Raw.Location);
			set {
				int location = GameHelper.CoordsToLocation(value);
				if (location == Raw.Location)
					return;

				if (location >= 0 && location < GameConstants.WorldWidth * GameConstants.WorldHeight) {
					Raw.Location = location;
					SetChanged();
				} else
					Log.Error($"Detect attempt for ${Raw.UserId} to move outside from world: from={Raw.Location} to={location}.");
			}
		}

		public int Rank {
			get => Raw.Rank;
			set {
				Raw.Rank = value;
				SetChanged();
			}
		}

		public int GuildId {
			get => Raw.GuildId;
			set {
				Raw.GuildId = value;
				SetChanged();
			}
		}
	}
}