using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;
using System;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
        [SerializeField]
        private Transform[] spawnPoints;

		private Dictionary<PlayerData.PlayerId, PlayerUnit> _players =
			new Dictionary<PlayerData.PlayerId, PlayerUnit> ();

		public void Init(params PlayerData[] players)
		{
            for (int i = 0; i < players.Length;i++) { 
				// Get prefab by UnitType
				PlayerUnit unitPrefab =
					Global.Instance.Prefabs.
					GetPlayerUnitPrefab (players[i].UnitType );

				if(unitPrefab != null)
				{
					// Initialize unit
					PlayerUnit unit = Instantiate ( unitPrefab, transform );
                    if (spawnPoints != null && spawnPoints[i] != null)
                    {
                        unit.transform.position = spawnPoints[i].position;
                    } else
                    {
                        unit.transform.position = Vector3.zero;
                    }
					unit.transform.rotation = Quaternion.identity;
					unit.Init (players[i]);

					// Add player to dictionary
					_players.Add (players[i].Id, unit );
				}
				else
				{
					Debug.LogError ( "Unit prefab with type " + players[i].UnitType +
						" could not be found!" );
				}
			}
		}

		public void UpdateMovement ( InputManager.ControllerType controller, 
			Vector3 input, bool shoot )
		{
			PlayerUnit playerUnit = null;
			foreach (var player in _players)
			{
				if(player.Value.Data.Controller == controller)
				{
					playerUnit = player.Value;
				}
			}

			if(playerUnit != null)
			{
				playerUnit.HandleInput ( input, shoot );
			}
		}

	}
}

