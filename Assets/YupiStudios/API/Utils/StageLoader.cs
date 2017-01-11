using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils {

	public class StageLoader : MonoBehaviour {

		public string sceneToLoad;
		
		// Update is called once per frame
		void Start () {
			Application.LoadLevel(sceneToLoad);
		}
	}

}