using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.Menu {

	public class PathEntryLogic : MonoBehaviour {

		public int PathNumber;
		public string SceneToLoad;
		
		private float minDist = 5.0f;
		private float proportionalDist;

		public float Distance(RectTransform b)
		{
			return Vector2.Distance(transform.position, b.position);
		}

		public RectTransform GetRectTransform()
		{
			return (RectTransform) transform;
		}

		public bool HasReached(RectTransform transform)
		{
			if ( Distance(transform) < minDist)
				return true;
			else
				return false;
		}

		public bool ReachDestiny(int num)
		{
			if (PathNumber == num)
				return true;
			else
				return false;

		}

		void Start()
		{
			minDist *= (Screen.height/480.0f) * (Screen.width/800.0f);
		}

	}	

}