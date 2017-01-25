using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKShooter.Systems
{
	public class Global : MonoBehaviour
	{
		private static Global _instance;

		public static Global Instance
		{
			get
			{
				if(_instance == null)
				{
					GameObject globalObj = new GameObject ( typeof ( Global ).Name );
					_instance = globalObj.AddComponent<Global> ();
				}

				return _instance;
			}
		}

		[SerializeField] private Prefabs _prefabs;

		public Prefabs Prefabs { get { return _prefabs; } }

		protected void Awake()
		{
			if(_instance == null)
			{
				// No instance set yet.
				// Let this object be our one and only instance.
				_instance = this;
			}

			if(_instance == this)
			{
				// This is the only allowed instance of the class.
				// Run initializations.
				Init ();
			}
			else
			{
				// Global is already instantiated! Destroy this instance.
				Destroy ( this );
			}
		}

		private void Init ()
		{
			if ( _prefabs == null )
			{
				_prefabs = GetComponentInChildren<Prefabs> ();
			}
		}
	}
}
