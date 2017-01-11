using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.Menu {

	public class CloudLogic : MonoBehaviour {

		public RectTransform[] clouds;
		public RectTransform minPosition;
		public RectTransform maxPosition;
		public float cloudSpeed;

		void Update()
		{
			foreach (RectTransform t in clouds)
			{
				Vector3 pos = t.position;
				pos.x -= cloudSpeed * Time.deltaTime;

				if (pos.x <= minPosition.position.x)
					pos.x = maxPosition.position.x;

				t.position = pos;
			}
		}

	}

}