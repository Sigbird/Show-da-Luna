using UnityEngine;
using System.Collections;

public class PositionScript : MonoBehaviour {
	public Sprite first;
	public Sprite second;
	public Sprite third;
	public int pos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pos == 1) {
			this.GetComponent<SpriteRenderer>().sprite = first;
		}
		if (pos == 2) {
			this.GetComponent<SpriteRenderer>().sprite = second;
		}
		if (pos == 3) {
			this.GetComponent<SpriteRenderer>().sprite = third;
		}
	}
}
