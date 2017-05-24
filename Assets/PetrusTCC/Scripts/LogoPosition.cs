using UnityEngine;
using System.Collections;

namespace YupiStudios.YupiPlay {

	public class LogoPosition : MonoBehaviour {
		public float marginRight;
		public float marginBottom;


		// Use this for initialization
		void Start () {
			SpriteRenderer logo = GetComponent<SpriteRenderer>();
			float size = logo.sprite.bounds.size.x;

			float x = Screen.width - size - marginRight * Screen.width;
			float y =  size + marginBottom * Screen.height;

			Vector3 logoPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));

			transform.position = logoPosition;		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
