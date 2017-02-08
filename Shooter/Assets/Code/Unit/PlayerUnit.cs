using System;
using TAMKShooter.Data;
using UnityEngine;
using TAMKShooter.Configs;

namespace TAMKShooter
{
	public class PlayerUnit : UnitBase
	{
		public enum UnitType
		{
			None = 0,
			Fast = 1,
			Balanced = 2,
			Heavy = 3
		}

		[SerializeField] private UnitType _type;
        private InputManager _inputManager;

        public UnitType Type { get { return _type; } }
		public PlayerData Data { get; private set; }

		public override int ProjectileLayer
		{
			get
			{
				return LayerMask.NameToLayer ( Config.PlayerProjectileLayerName );
			}
		}

		public void Init( PlayerData playerData, InputManager inputManager )
		{
			Data = playerData;
            _inputManager = inputManager;
            _inputManager.AddPlayer(this, Data.ControllerType);
		}

		protected override void Die ()
		{
			// TODO: Handle dying properly!
			gameObject.SetActive ( false );

			base.Die ();
		}

		protected void Update()
		{

			Mover.MoveToDirection ( _inputManager.GetInputAxes(this) );

            bool shoot = _inputManager.GetShoot(this);
			if(shoot)
			{
				Weapons.Shoot ( ProjectileLayer );
			}
		}
	}
}
