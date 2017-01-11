using UnityEngine;
using System.Collections;


namespace YupiStudios.Luna.InGame.BackyardUI {

	public class CurtainClothLink : MonoBehaviour {

		public Transform toFollow;
		public bool invertMovement;
		public bool isSprite;

		private Vector3 startPos;
		private Vector3 toFollowStartPos;

		private Vector3 toFollowLastPos;
		private Vector3 lastPos;

		private Vector3 zeroCam;

		private void ChangeCurrentPos()
		{
			Vector3 desloc = Vector3.zero;
			
			if (isSprite) 
			{
				desloc = Camera.main.ScreenToWorldPoint(toFollow.localPosition - toFollowStartPos) - zeroCam;
			} 
			else
			{
				desloc = toFollow.localPosition - toFollowStartPos;
			}
			
			if (invertMovement)
				desloc *= -1;
			
			transform.localPosition = startPos  + desloc;
			lastPos = transform.localPosition;
		}

		private void ChangeFollowerPos()
		{
			Vector3 desloc = Vector3.zero;
			
			if (isSprite) 
			{
				desloc = Camera.main.ScreenToWorldPoint(transform.localPosition - startPos) - zeroCam;
			} 
			else
			{
				desloc = transform.localPosition - startPos;
			}
			
			if (invertMovement)
				desloc *= -1;
			
			toFollow.localPosition = toFollowStartPos  + desloc;
			toFollowLastPos = toFollow.localPosition;
		}

		// Use this for initialization
		void Awake () {
			zeroCam = Camera.main.ScreenToWorldPoint (Vector3.zero);
		}

		void Start()
		{
			startPos = transform.localPosition;
			toFollowStartPos = toFollow.localPosition;
			lastPos = transform.localPosition;
		}
		
		// Update is called once per frame
		void LateUpdate () {

			/*if (firstUpdate) {
				startPos = transform.localPosition;
				toFollowStartPos = toFollow.localPosition;
				lastPos = transform.localPosition;
				firstUpdate = false;
			}*/

			if (toFollow.localPosition != toFollowLastPos) {
				toFollowLastPos = toFollow.localPosition;
				ChangeCurrentPos();
			}

			if (transform.localPosition != lastPos) {
				lastPos = transform.localPosition;
				ChangeFollowerPos();
			}

		}
	}

}