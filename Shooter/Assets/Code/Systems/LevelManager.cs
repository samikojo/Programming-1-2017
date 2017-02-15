using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using System;

namespace TAMKShooter.Systems
{
	public class LevelManager : SceneManager
	{
		// Add reference to InputManager here.
		public PlayerUnits PlayerUnits { get; private set; }
		public EnemyUnits EnemyUnits { get; private set; }
        public InputManager InputManager { get; private set; }

		public override GameStateType StateType
		{
			get { return GameStateType.InGameState; }
		}

		protected void Awake()
		{
			Initialize ();
		}

		private void Initialize()
		{
			PlayerUnits = GetComponentInChildren<PlayerUnits> ();
			EnemyUnits = GetComponentInChildren<EnemyUnits> ();
            InputManager = GetComponentInChildren<InputManager>();

			EnemyUnits.Init ();

            // TODO: Get player data from GameManager (new data or saved data)
            PlayerData[] playerData = new PlayerData[4];

            for (int i = 0; i < playerData.Length; i++)
            {
                playerData[i] = new PlayerData()
                {
                    Id = (PlayerData.PlayerId)i + 1,
                    UnitType = (PlayerUnit.UnitType)Random.Range(1,4),
                    ControllerType = (PlayerData.PlayerControllerType)Random.Range(1, 3),
                    //ControllerType = PlayerData.PlayerControllerType.Keyboard,
                    Lives = 3
                };
            }

            PlayerUnits.Init ( InputManager, playerData);
		}
	}
}
