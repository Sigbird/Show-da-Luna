using UnityEngine;
using System.Collections;

public class FlipTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(-1,1));
	}
}
