using UnityEngine;
using System.Collections;

public class BirdCollider : MonoBehaviour {

	public AudioClip dano;
	public bool invincible;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {	       
		if (other.tag == "Airplane" && invincible == false) {
			StartCoroutine(Blink());
			GameController.Vida = GameController.Vida - 1;
			GameController.audio.PlayOneShot (dano,0.5f);
		}

		if (other.tag == "Branch" && invincible == false) {
			StartCoroutine(Blink());
			GameController.Vida = GameController.Vida - 1;
			other.transform.GetChild(0).GetComponent<Animator>().SetTrigger("shake");
			GameController.audio.PlayOneShot (dano,0.5f);
		}

		if (other.tag == "Feather") {
			//Debug.Log ("Airplane");
			if(GameController.Vida<3){
				GameController.Vida = GameController.Vida + 1;
			}
			Destroy(other.gameObject);
		}
	    

		
	}

	IEnumerator Blink(){
		GameObject.Find ("BirdSprite").GetComponent<Animator> ().SetTrigger ("Hit");
		GameObject.Find ("BirdSprite").GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds(0.2f);
		GameObject.Find ("BirdSprite").GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds(0.2f);
		GameObject.Find ("BirdSprite").GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds(0.2f);
		GameObject.Find ("BirdSprite").GetComponent<SpriteRenderer> ().enabled = true;
	}
}
