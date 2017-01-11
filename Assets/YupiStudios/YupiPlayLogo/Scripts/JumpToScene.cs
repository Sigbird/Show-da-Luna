using UnityEngine;
using System.Collections;


namespace YupiStudios.YupiPlayLogo {

	public class JumpToScene : MonoBehaviour {

		[Tooltip("If not empty, jump to this scene on Start")]
		public string sceneName;

		public void LoadScene(string sceneName)
		{
			Application.LoadLevel (sceneName);
		}

		// Use this for initialization
		void Start () {
			if ( !string.IsNullOrEmpty( sceneName ) )
				Application.LoadLevel (sceneName);
		}

	}


}