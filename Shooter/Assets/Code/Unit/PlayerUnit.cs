using System;
using TAMKShooter.Data;
using UnityEngine;
using TAMKShooter.Configs;
using System.Collections;

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
        public float InvincibilityDuration = 1f;

		[SerializeField] private UnitType _type;

        private bool _invincibility;
        private float _baseTime;
        private MeshRenderer _renderer;

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
            Mover.Position = Data.SpawnPoint;
            Mover.Rotation = Quaternion.identity;
            _renderer = GetComponent<MeshRenderer>();
		}

		protected override void Die ()
		{
            Data.Lives--;

            if (Data.Lives <= 0) {
                // TODO: Handle dying properly!
                // Instantiate explosion effect
                // Play sound
                // Decrease lives
                // Respawn player
                gameObject.SetActive(false);

                base.Die();
            }
            else
            {
                Respawn();
            }
		}

        protected override void Respawn()
        {
            Mover.Position = Data.SpawnPoint;
            Mover.Rotation = Quaternion.identity;
            Health.CurrentHealth = 100;
            _baseTime = Time.time;
            StartInvincibility();

            base.Respawn();
        }

        public void HandleInput ( Vector3 input, bool shoot )
		{
			Mover.MoveToDirection ( input );
			if(shoot)
			{
				Weapons.Shoot (ProjectileLayer);
			}
		}

        protected override void OnCollisionStay(Collision coll)
        {
            if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && !_invincibility)
            {
                Health.CurrentHealth -= 25;
            }

            base.OnCollisionStay(coll);

        }

        private void StartInvincibility()
        {
            _invincibility = true;
            StartCoroutine(Invincibility());
        }

        private IEnumerator Invincibility()
        {
            while (true)
            {
                _renderer.enabled = !_renderer.enabled;

                if (Time.time - _baseTime > InvincibilityDuration)
                {
                    EndInvincibility();
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void EndInvincibility()
        {
            _invincibility = false;
            _renderer.enabled = true;
        }
    }
}
