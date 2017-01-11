using UnityEngine;
using System.Collections;

public class AirplaneLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyIt(){
		Destroy(transform.parent.gameObject);
	}
}
