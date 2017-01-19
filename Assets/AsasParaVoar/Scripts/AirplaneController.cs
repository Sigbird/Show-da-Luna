using UnityEngine;
using System.Collections;

public class AirplaneController : MonoBehaviour {

	public bool revert;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//Destroy (gameObject, 30);
	}

	void FixedUpdate() {		
		if (!revert) {
			rb.velocity = rb.velocity + (-Vector2.up * Time.deltaTime * 0.9f) + (-Vector2.right * Time.deltaTime * 0.7f);
			//rb.MovePosition((Vector2) this.transform.position + (-Vector2.up * Time.deltaTime * 0.9f) + (-Vector2.right * Time.deltaTime * 0.7f));	
		} else {
			rb.velocity = rb.velocity + (-Vector2.up * Time.deltaTime * 0.9f) + (Vector2.right * Time.deltaTime * 0.7f);
			//rb.MovePosition((Vector2) this.transform.position + (-Vector2.up * Time.deltaTime * 0.9f) + (Vector2.right * Time.deltaTime * 0.7f));	
		}
	}

	// Update is called once per frame
	void Update () {

//		if (!revert) {
//			this.transform.Translate (-Vector2.up * Time.deltaTime * 0.9f);
//			this.transform.Translate (-Vector2.right * Time.deltaTime * 0.7f);
//		} else {
//			this.transform.Translate (-Vector2.up * Time.deltaTime * 0.9f);
//			this.transform.Translate (Vector2.right * Time.deltaTime * 0.7f);
//		}
	
	}



}
