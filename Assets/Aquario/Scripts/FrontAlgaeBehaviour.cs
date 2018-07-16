using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
	public class FrontAlgaeBehaviour : MonoBehaviour {
		
		private Animator animator;
		// Use this for initialization
		void Start () {
			animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update () {

		}

		void OnTriggerEnter2D(Collider2D coll) {			
			Reveal(coll.gameObject.tag, true);
		}

		void OnTriggerExit2D(Collider2D coll) {			
			Reveal(coll.gameObject.tag, false);
		}			

		private void Reveal(string tag, bool state) {
			if (tag == "Player") {
				animator.SetBool ("IsRevealed", state);
			}
		}
	}

}
