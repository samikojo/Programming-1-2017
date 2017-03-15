using System;
using System.Collections.Generic;
using TAMKShooter.Data;
using UnityEngine;

namespace TAMKShooter.Systems
{
	public class MenuManager : SceneManager
	{
		public override GameStateType StateType
		{
			get { return GameStateType.MenuState; }
		}

		public void StartGame()
		{
			Global.Instance.CurrentGameData = new GameData()
			{
				Level = 1,
				PlayerDatas = new List< PlayerData >()
				{
					new PlayerData()
					{
						Controller = InputManager.ControllerType.KeyboardArrow,
						Lives = 3,
						Id = PlayerData.PlayerId.Player1,
						UnitType = PlayerUnit.UnitType.Balanced
					},
					new PlayerData()
					{
						Controller = InputManager.ControllerType.KeyboardWasd,
						Lives = 3,
						Id = PlayerData.PlayerId.Player2,
						UnitType = PlayerUnit.UnitType.Heavy
					}
				}
			};

			Global.Instance.GameManager.
				PerformTransition ( GameStateTransitionType.MenuToInGame );
		}

		public void LoadGame()
		{
			Debug.Log ( "Load Game" );
		}

		public void QuitGame ()
		{
			Application.Quit ();
		}
	}
}
