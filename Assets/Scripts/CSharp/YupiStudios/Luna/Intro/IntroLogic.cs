using UnityEngine;
using System.Collections;


namespace YupiStudios.Luna.Intro {

	public class IntroLogic : MonoBehaviour {

		public string[] levelsToLive; 

		private static IntroLogic currentIntro;

		// Use this for initialization
		void Start () {
			Screen.fullScreen = true;

			if (currentIntro == null) {
				currentIntro = this;
			}

			if (this != currentIntro)
				Destroy (gameObject);

			DontDestroyOnLoad (gameObject);
		}

		void OnLevelWasLoaded() {
			ArrayList list = new ArrayList ();
			list.AddRange(levelsToLive);
			if (!list.Contains (Application.loadedLevelName)) {
				currentIntro = null;
				Destroy(gameObject);
			}
		}

	}

}
