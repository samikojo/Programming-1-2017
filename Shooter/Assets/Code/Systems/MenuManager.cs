using UnityEngine;

namespace TAMKShooter.Systems
{
	public class MenuManager : SceneManager
	{
		public void StartGame()
		{
			Debug.Log ( "Start Game" );
		}

		public void LoadGame()
		{
			Debug.Log ( "Load Game" );
		}

		public void QuitGame ()
		{
			Application.Quit ();
		}
	}
}
