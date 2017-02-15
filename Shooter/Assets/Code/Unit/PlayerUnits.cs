using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
		private Dictionary<PlayerData.PlayerId, PlayerUnit> _players =
			new Dictionary<PlayerData.PlayerId, PlayerUnit> ();

		public void Init(params PlayerData[] players)
		{
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
					unit.transform.position = Vector3.zero;
					unit.transform.rotation = Quaternion.identity;
					unit.Init ( playerData );
				    unit.InputSource = InputManager.GetController(playerData.ControllerType);

				    if (unit.InputSource == null)
				    {
				        Debug.LogError("No free preferred controller for this player: " + playerData.ControllerType);
				        gameObject.SetActive(false);
				        Destroy(gameObject);
				    }

				    unit.InputSource.FireButton = playerData.FireButton;

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

		// Update player movement


	}
}

