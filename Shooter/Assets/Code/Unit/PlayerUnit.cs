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

		public UnitType Type { get { return _type; } }
		public PlayerData Data { get; private set; }
	    public Vector3 SpawnPoint = Vector3.zero;

	    private MeshRenderer _meshRenderer;
	    private Collider _collider;
	    private float _lastFlicker;
	    private float _immunityTime;

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
		    _meshRenderer = GetComponent<MeshRenderer>();
		    _collider = GetComponent<Collider>();
		}

	    void Update()
	    {
	        if (_immunityTime > 0)
	        {
	            if (_lastFlicker > 0.1f)
	            {
                    _meshRenderer.enabled = !_meshRenderer.enabled;
	                _lastFlicker = 0;
	            }
	            _lastFlicker += Time.deltaTime;
                _immunityTime -= Time.deltaTime;
	        } else if (_collider.enabled == false)
	        {
	            _collider.enabled = true;
	            _meshRenderer.enabled = true;
	        }
	    }

		protected override void Die ()
		{
			// TODO: Handle dying properly!
			// Instantiate explosion effect
			// Play sound
			// Decrease lives
			// Respawn player

		    if (Data.Lives > 0)
		    {
		        Data.Lives--;
		        transform.position = SpawnPoint;
		        _immunityTime = 2;
		        _collider.enabled = false;
		    }
		    else
		    {
                gameObject.SetActive(false);
            }
		    

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

	    void OnTriggerEnter(Collider other)
	    {
	        Debug.Log("TRIGGERED");
	        Die();
	    }
	}
}
