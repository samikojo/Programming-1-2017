using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Systems;

namespace TAMKShooter
{
	public class PlayerUnits : MonoBehaviour
	{
		public Dictionary<PlayerData.PlayerId, PlayerUnit> Players =
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

					// Add player to dictionary
					Players.Add ( playerData.Id, unit );
                    Debug.Log("INIT");
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

