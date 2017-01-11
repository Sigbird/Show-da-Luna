using UnityEngine;
using System.Collections;

public class AirplaneController : MonoBehaviour {

	public bool revert;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 30);
	}
	
	// Update is called once per frame
	void Update () {

		if (!revert) {
			this.transform.Translate (-Vector2.up * Time.deltaTime * 0.9f);
			this.transform.Translate (-Vector2.right * Time.deltaTime * 0.7f);
		} else {
			this.transform.Translate (-Vector2.up * Time.deltaTime * 0.9f);
			this.transform.Translate (Vector2.right * Time.deltaTime * 0.7f);
		}
	
	}



}
