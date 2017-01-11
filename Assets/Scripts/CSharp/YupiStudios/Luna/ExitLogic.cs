using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitLogic : MonoBehaviour {

	public GameObject exitWindow;


	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			exitWindow.SetActive(true);
		}
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
