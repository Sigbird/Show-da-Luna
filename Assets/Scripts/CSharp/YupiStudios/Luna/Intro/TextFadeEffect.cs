using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace YupiStudios.Luna.Intro {

	[RequireComponent(typeof(UnityEngine.UI.Text))]
	public class TextFadeEffect : MonoBehaviour {

		private float blinkTimer;
		private Text textComp;

		void Start () {
			textComp = GetComponent<UnityEngine.UI.Text> ();
		}
		
		
		// Update is called once per frame
		void Update () {
			
			blinkTimer += Time.deltaTime * 2;
			if (blinkTimer > 200)
				blinkTimer = 0;
			Color color = textComp.color;
			color.a = Mathf.Sin (blinkTimer) + 1;
			textComp.color = color;
			
		}

	}

}