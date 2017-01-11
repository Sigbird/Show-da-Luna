using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.InGame.BackyardUI {

	public class CurtainLogic : MonoBehaviour {

		private const float CURTAIN_OPENED_SIZE = 1.1f;
		private const float CURTAIN_CLOSED_SIZE = 1.0f;
		private const float CHANGE_SIZE_SPEED = 1f;

		private float changeTime;

		private Vector3 openedSize;
		private Vector3 closedSize;

		private Vector3 Size
		{
			get 
			{
				return transform.localScale;
			}
			set
			{
				transform.localScale = value;
			}
		}


		private bool LargeSize {
			get;
			set;
		}

		public void Shrink()
		{
			LargeSize = false;
			changeTime = 0;
		}

		public void Enlarge()
		{
			LargeSize = true;
			changeTime = 0;
		}

		void Start ()
		{
			openedSize = Size;
			closedSize = Size;
			changeTime = 0;
			
			openedSize.x *= CURTAIN_OPENED_SIZE;
			openedSize.y *= CURTAIN_OPENED_SIZE;
			openedSize.z *= CURTAIN_OPENED_SIZE;
			
			closedSize.x *= CURTAIN_CLOSED_SIZE;
			closedSize.y *= CURTAIN_CLOSED_SIZE;
			closedSize.z *= CURTAIN_CLOSED_SIZE;

			LargeSize = false;
		}

		void Update()
		{

			if ( LargeSize ) {

				Vector3 newSize = Size;
				if (newSize.x != openedSize.x) {
					
					newSize.x = Mathf.Lerp(newSize.x, openedSize.x,changeTime);
					newSize.y = Mathf.Lerp(newSize.y, openedSize.y,changeTime);

					Size = newSize;
					
					changeTime += Time.deltaTime * CHANGE_SIZE_SPEED;
					if (changeTime > 1)
						changeTime = 1;
				}
			} else {
				
				Vector3 newSize = Size;
				if (newSize.x != closedSize.x) {

					newSize.x = Mathf.Lerp(newSize.x, closedSize.x,changeTime);
					newSize.y = Mathf.Lerp(newSize.y, closedSize.y,changeTime);

					Size = newSize;
					
					changeTime += Time.deltaTime * CHANGE_SIZE_SPEED;
					if (changeTime > 1)
						changeTime = 1;
				}				

			}

		}

	}


}