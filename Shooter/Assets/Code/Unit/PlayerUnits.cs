using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;
using System;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
	    private SpawnPoints _spawns;

		private Dictionary<PlayerData.PlayerId, PlayerUnit> _players =
			new Dictionary<PlayerData.PlayerId, PlayerUnit> ();

		public void Init(params PlayerData[] players)
		{
		    _spawns = FindObjectOfType<SpawnPoints>();

			foreach (PlayerData playerData in players)
			{
				// Get prefab by UnitType
				PlayerUnit unitPrefab =
					Global.Instance.Prefabs.
					GetPlayerUnitPrefab ( playerData.UnitType );

				if(unitPrefab != null)
				{
					// Initialize unit
					PlayerUnit unit = Instantiate ( unitPrefab, transform );

				    if (_spawns != null) unit.SpawnPoint = _spawns.GetSpawnPoint((int)playerData.Id - 1);
				    unit.transform.position = unit.SpawnPoint;
					unit.transform.rotation = Quaternion.identity;
					unit.Init ( playerData );

					// Add player to dictionary
					_players.Add ( playerData.Id, unit );
				}
				else
				{
					Debug.LogError ( "Unit prefab with type " + playerData.UnitType +
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

