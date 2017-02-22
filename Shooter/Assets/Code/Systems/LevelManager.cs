using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Utility;
using System;
using TAMKShooter.Level;
using TAMKShooter.Systems.States;

namespace TAMKShooter.Systems
{
	public class LevelManager : SceneManager
	{
		private ConditionBase[] _conditions;

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

			EnemyUnits.Init ();

			// TODO: Get player data from GameManager (new data or saved data)
			PlayerData playerData1 = new PlayerData ()
			{
				Id = PlayerData.PlayerId.Player1,
				UnitType = PlayerUnit.UnitType.Heavy,
				Lives = 3,
				Controller = InputManager.ControllerType.KeyboardWasd
			};

			PlayerData playerData2 = new PlayerData ()
			{
				Id = PlayerData.PlayerId.Player2,
				UnitType = PlayerUnit.UnitType.Balanced,
				Lives = 3,
				Controller = InputManager.ControllerType.KeyboardArrow
			};

			PlayerData playerData3 = new PlayerData ()
			{
				Id = PlayerData.PlayerId.Player3,
				UnitType = PlayerUnit.UnitType.Fast,
				Lives = 3,
				Controller = InputManager.ControllerType.Gamepad1
			};

			PlayerUnits.Init ( playerData1, playerData2, playerData3 );

			InputManager = gameObject.GetOrAddComponent<InputManager> ();
			InputManager.Init ( this, InputManager.ControllerType.KeyboardWasd,
				InputManager.ControllerType.KeyboardArrow,
				InputManager.ControllerType.Gamepad1 );

			// All conditions should be parented to LevelManager
			_conditions = GetComponentsInChildren<ConditionBase> ();
			foreach(var condition in _conditions)
			{
				condition.Init ( this );
			}
		}

		public void ConditionMet ( ConditionBase condition )
		{
			bool areConditionsMet = true;
			foreach(ConditionBase c in _conditions)
			{
				if(!c.IsConditionMet)
				{
					areConditionsMet = false;
					break;
				}
			}

			if(areConditionsMet)
			{
				( AssociatedState as GameState ).LevelCompleted ();
			}
		}

		public void UpdateMovement ( InputManager.ControllerType controller,
			Vector3 input, bool shoot )
		{
			PlayerUnits.UpdateMovement ( controller, input, shoot );
		}
	}
}
