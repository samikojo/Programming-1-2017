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
        [SerializeField] private bool invulnerable = false;
        [SerializeField] private float invulnerabilityTime;

        private float flashingTime;
        private Vector3 originalPosition;
        private float runningTime = 0.0f;

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
            originalPosition = transform.position;
		}

		protected override void Die ()
		{
            // TODO: Handle dying properly!
            // Instantiate explosion effect
            // Play sound
            // Decrease lives
            // Respawn player

            if (Data.Lives == 0) {
                gameObject.SetActive ( false );
            }

            Debug.Log("Player died");
            transform.position = originalPosition;
            Invulnerability();

			base.Die ();
		}

		public void HandleInput ( Vector3 input, bool shoot )
		{
			Mover.MoveToDirection ( input );
			if(shoot)
			{
				Weapons.Shoot (ProjectileLayer);
			}
		}

        public void OnCollisionEnter(Collision col) {
            if (col.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                if (!invulnerable) {
                    Debug.Log("Hit Enemy!");
                    Data.Lives -= 1;
                    Die();
                }
            }
        }

        public void Invulnerability() {
            invulnerable = true;
        }

        void Update() {
            if (invulnerable) {
                runningTime += Time.deltaTime;
                flashingTime += Time.deltaTime;

                if (runningTime >= invulnerabilityTime) {
                    invulnerable = false;
                    runningTime = 0.0f;
                    flashingTime = 0.0f;
                    GetComponent<MeshRenderer>().enabled = true;
                } else {
                    if (flashingTime < 0.05f && flashingTime > 0.0f) {
                        GetComponent<MeshRenderer>().enabled = true;
                    } else if (flashingTime >= 0.05f && flashingTime <= 0.1f) {
                        GetComponent<MeshRenderer>().enabled = false;
                    } else {
                        flashingTime = 0.0f;
                    }
                }
            }
        }
	}
}
