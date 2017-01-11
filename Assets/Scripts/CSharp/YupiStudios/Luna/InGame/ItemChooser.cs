using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace YupiStudios.Luna.InGame {

	public class ItemChooser : MonoBehaviour {

		public string path;
		public string []prefabNames;
		public bool isRandom = true;
		private GameObject currentItem;		                             

		public void DestroyItem ()
		{
			if (currentItem != null)
			{
				GameObject.Destroy(currentItem);
				currentItem = null;
			}
		}

		public Color GetColor()
		{
			return currentItem.GetComponentInChildren<ColorableObject>().GetColor();
		}

		public void SetColor(Color c)
		{
			currentItem.GetComponentInChildren<ColorableObject>().SetColor(c);
		}

		public string LoadItem(int itemNum, List<string> loadedItems, int randModifier = 0)
		{


			if (prefabNames.Length > 0)
			{
				int num = itemNum % prefabNames.Length;
				if (isRandom)
				{
					Random.seed = itemNum+randModifier;
					num = Random.Range(0,prefabNames.Length);
				}

				string name = prefabNames[num];

				while (loadedItems.Contains(name))
				{
					name = prefabNames[(num++) % prefabNames.Length];
				}
				string fullPath = path+name;
				currentItem = (GameObject)Instantiate((GameObject)Resources.Load(fullPath, typeof(GameObject)));
				currentItem.transform.parent = transform;
				currentItem.transform.localPosition = Vector3.zero;
				return name;
			}

			return null;
		}

	}

}