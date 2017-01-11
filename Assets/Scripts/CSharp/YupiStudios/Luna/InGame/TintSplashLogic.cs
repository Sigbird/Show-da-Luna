using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame {

	public class TintSplashLogic : MonoBehaviour {

		private SpriteRenderer [] sprites;
		private Animator animator;

		public void SetColor (Color c) {
			foreach (SpriteRenderer spRenderer in sprites) {
				spRenderer.color = c;
			}
		}

		private IEnumerator RunAnim() 
		{
			animator.gameObject.SetActive (false);
			yield return new WaitForEndOfFrame();
			animator.gameObject.SetActive (true);
		}

		public void PlayAnim()
		{
			if (gameObject.activeInHierarchy)
				StartCoroutine (RunAnim());
		}

		void Start () {
			sprites = GetComponentsInChildren<SpriteRenderer> ();
			animator = GetComponentInChildren<Animator> ();
			SetColor (Color.white);
			animator.gameObject.SetActive (false);
		}
	}

}