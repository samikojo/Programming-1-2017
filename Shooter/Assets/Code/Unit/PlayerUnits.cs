using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;
using System;
using System.Linq;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
		private Dictionary<PlayerData.PlayerId, PlayerUnit> _players =
			new Dictionary<PlayerData.PlayerId, PlayerUnit> ();
        [SerializeField] private Transform playerSpawnPointsParent;
        

		public void Init(params PlayerData[] players)
		{
            var playerSpawnPoints = playerSpawnPointsParent.GetComponentsInChildren<Transform>();
            int currentSpawnPointIndex = 0;

            foreach (PlayerData playerData in players)
			{
				// Get prefab by UnitType
				PlayerUnit unitPrefab =
					Global.Instance.Prefabs.
					GetPlayerUnitPrefab ( playerData.UnitType );

				if(unitPrefab != null)
				{
                    Transform spawnPoint = transform;

                    if (currentSpawnPointIndex >= playerSpawnPoints.Length)
                    {
                        if (playerSpawnPoints.Length == 0)
                        {
                            new UnityException("PlayerSpawnPoints array lenght is 0. SpawnPoints not set in scene. Players spawn into PlayerUnits");
                        } else
                        {
                            spawnPoint = playerSpawnPoints[--currentSpawnPointIndex];
                            new UnityException("PlayerSpawnPoints array lenght is less than 4. Player spawnPoints overlap.");
                        }
                    } else
                    {
                        spawnPoint = playerSpawnPoints[currentSpawnPointIndex];
                        currentSpawnPointIndex++;
                    }
                    

					// Initialize unit
					PlayerUnit unit = Instantiate ( unitPrefab, transform );
					unit.transform.position = spawnPoint.position;
					unit.transform.rotation = Quaternion.identity;
					unit.Init ( playerData, spawnPoint );

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

