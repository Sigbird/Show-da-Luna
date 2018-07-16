using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Luna.Aquario 
{
public class InteractibleItens : MonoBehaviour {

	public int interactible;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
			if (Input.GetMouseButtonUp (0) ) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0;
			if (Vector3.Distance(transform.position,mousePos) <= 1) {
				gameObject.GetComponent<Animator> ().SetTrigger ("Interact");
				switch (interactible) {
				case 0:
					GameController.Instance.PlayAudio (4);
					break;
				case 1:
					GameController.Instance.PlayAudio (5);
					break;
				case 2:
					GameController.Instance.PlayAudio (6);
					break;
				default:
					break;
				}
				}

			}

	}

}
}