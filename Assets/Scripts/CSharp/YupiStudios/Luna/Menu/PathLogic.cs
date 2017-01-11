using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.Menu {

	public class PathLogic : MonoBehaviour {

		private PathEntryLogic []PathEntries;


		public PathEntryLogic GetPathEntryByLoadscene (string sceneName)
		{
			if (string.IsNullOrEmpty (sceneName))
				return null;

			foreach (PathEntryLogic entry in PathEntries) {
				if (entry.SceneToLoad == sceneName)
				{
					return entry;
				}
			}

			return null;
		}

		public float Distance(RectTransform a, RectTransform b)
		{
			return Vector2.Distance(a.position, b.position);
		}

		public PathEntryLogic GetClosestPath(RectTransform currentPos, PathEntryLogic currentPath, PathEntryLogic destiny)
		{		
			if ( currentPath.HasReached(currentPos) )
			{
				if (currentPath.ReachDestiny(destiny.PathNumber))
					return null;

				int inc = 0;

				if (currentPath.PathNumber < destiny.PathNumber) 
				{
					inc = 1;
				}
				else if (currentPath.PathNumber > destiny.PathNumber)
				{
					inc = -1;
				}

				return PathEntries[currentPath.PathNumber+inc];
			}
			else
				return currentPath;
		}

		public PathEntryLogic GetClosestPath(RectTransform currentPos, PathEntryLogic destiny)
		{

			float closestDist1 = 0;
			int firstI = 0;

			bool hasFirst = false;

			for (int i = 0; i < PathEntries.Length; ++i)
			{
				float plDist = Distance(currentPos, PathEntries[i].GetRectTransform());

				if (!hasFirst)
				{
					hasFirst = true;
					firstI = i;
					closestDist1 = plDist;
					continue;
				}


				if (plDist < closestDist1)
				{
					closestDist1 = plDist;
					firstI = i;
				}
			}

			PathEntryLogic closest = PathEntries[firstI];

			return closest;
		}

		public RectTransform GetFirst ()
		{
			return PathEntries [0].GetRectTransform(); //Inicio do caminho
		}

		void Awake()
		{

			PathEntries = GetComponentsInChildren<PathEntryLogic>();

			System.Array.Sort(PathEntries, 
			                  delegate(PathEntryLogic p1, PathEntryLogic p2) {
											return p1.PathNumber.CompareTo(p2.PathNumber);
										});

		}

	}

}
