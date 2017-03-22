using System;
using TAMKShooter.Data;
using UnityEngine;
using TAMKShooter.Configs;

namespace TAMKShooter
{
	public class PlayerUnit : UnitBase
	{
	    public Vector3 SpawnPoint = Vector3.zero;

		public enum UnitType
		{
			None = 0,
			Fast = 1,
			Balanced = 2,
			Heavy = 3
		}

		[SerializeField] private UnitType _type;

		public UnitType Type { get { return _type; } }
		public PlayerData Data { get; private set; }

		public override int ProjectileLayer
		{
			get
			{
				return LayerMask.NameToLayer ( Config.PlayerProjectileLayerName );
			}
		}

		public void Init( PlayerData playerData )
		{
			InitRequiredComponents();
			Data = playerData;
		}

		protected override void Die ()
		{
			// TODO: Handle dying properly!
			// Instantiate explosion effect
			// Play sound

		    if (--Data.Lives > 0)
		    {
		        transform.position = SpawnPoint;
		    }
		    else
		    {
		        gameObject.SetActive(false);
		        base.Die();
		    }
		}

		public void HandleInput ( Vector3 input, bool shoot )
		{
			Mover.MoveToDirection ( input );
			if(shoot)
			{
				Weapons.Shoot (ProjectileLayer);
			}
		}

	    private void OnTriggerEnter(Collider other)
	    {
	        var health = other.gameObject.GetComponent<Health>();
	        if (health != null) health.TakeDamage(health.CurrentHealth);

	        Die();
	    }
	}
}
