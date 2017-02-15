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

			EnemyUnits.Init ();

			// TODO: Get player data from GameManager (new data or saved data)
			PlayerData playerData = new PlayerData ()
			{
				Id = PlayerData.PlayerId.Player1,
				UnitType = PlayerUnit.UnitType.Heavy,
				Lives = 3
			};

			PlayerUnits.Init ( playerData );
		}
	}
}
