using System;

namespace TAMKShooter.Data
{
	[Serializable]
	public class PlayerData
	{
		public enum PlayerId
		{
			None = 0,
			Player1 = 1,
			Player2 = 2,
			Player3 = 3,
			Player4 = 4
		}

        public enum PlayerControllerType
        {
            None = 0,
            Keyboard = 1,
            Controller = 2
        }

		public PlayerId Id;
		public PlayerUnit.UnitType UnitType;
        public PlayerControllerType ControllerType;
		public int Lives;
	}
}
