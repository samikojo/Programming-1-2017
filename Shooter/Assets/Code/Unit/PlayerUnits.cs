using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TAMKShooter.Data;

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
				// Initialize unit
				// Add player to dictionary
			}
		}

		// Update player movement


	}
}

