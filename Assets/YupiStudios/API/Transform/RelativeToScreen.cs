using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Transform 
{


	public class RelativeToScreen : MonoBehaviour {

		public Vector2 relativePos = Vector2.zero;

		public Vector2 relativeSize = Vector2.one;

		public bool saveAspectRatio = true;

		public bool useSize = true;
		public bool usePos = true;

		public bool onUpdate = false;

		private void UpdateObject() {
			if (usePos)
			{
				Vector3 worldPosition = Camera.main.ViewportToWorldPoint(relativePos);
				worldPosition.z = transform.position.z;
				transform.position = worldPosition;
			}
			
			if (useSize)
			{
				Vector3 worldPosition1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.transform.position.z));
				Vector3 worldPosition2 = Camera.main.ViewportToWorldPoint(new Vector3(1,1,Camera.main.transform.position.z));
				
				float diffX = Mathf.Abs(worldPosition2.x -worldPosition1.x);
				float diffY = Mathf.Abs(worldPosition2.y -worldPosition1.y);
				
				if (saveAspectRatio)
				{
					if (diffX < diffY)
						diffY = diffX;
					else
						diffX = diffY;
				}

				Vector3 relatSize3D = relativeSize;
				relatSize3D.z = 1;
				
				Vector3 size = Vector3.Scale(new Vector3(diffX,diffY,1),relatSize3D);
				transform.localScale = size;
			}
		}

		void Update() {
			if (onUpdate)
				UpdateObject();
		}

		// Use this for initialization
		void Start () {
			UpdateObject();
		}
	}


}