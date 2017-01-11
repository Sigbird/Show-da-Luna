using UnityEngine;
using System.Collections;

public class TrailScript : MonoBehaviour {
	//public bool vertical;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {

		this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(this.GetComponent<SpriteRenderer>().color.a,0,Time.deltaTime * 1)); 

	}


}
