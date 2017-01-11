using UnityEngine;
using System.Collections;

public class CallYupiGames : MonoBehaviour {

	public void CallGame(string url){
		Application.OpenURL(url);
	}
}
