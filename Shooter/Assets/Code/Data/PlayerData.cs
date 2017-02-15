using System;
using TAMKShooter;

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

        public enum ControllerCodes
        {
            None, K1, K2, C1, C2
        }

        public PlayerId Id;
		public PlayerUnit.UnitType UnitType;
        public ControllerCodes ControllerCode;
		// TODO: Controller type
		public int Lives;
	}
}
