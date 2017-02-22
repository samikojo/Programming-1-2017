using UnityEngine;

namespace TAMKShooter.GUI
{
	public class GlobalGUI : MonoBehaviour
	{
		protected void Awake()
		{
			DontDestroyOnLoad ( gameObject );
		}
	}
}
