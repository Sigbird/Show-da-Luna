using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils {

	[RequireComponent(typeof(BoxCollider2D))]
	public class ActionOnClickCollider : MonoBehaviour {

		private BoxCollider2D listenerRegion;
		public ActionObject action;

		void Awake()
		{
			listenerRegion = GetComponent<BoxCollider2D> ();
		}

		private Vector2 PixelCoordFromMousePos()
		{
			
			if (listenerRegion != null)
			{			
				Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				
				Bounds b = listenerRegion.bounds;
				
				Vector2 v = new Vector2( (mouse.x - b.min.x) / ((float)b.size.x) , (mouse.y - b.min.y) / ((float)b.size.y) );
				
				return v;
			} else
			{
				return new Vector2(-1,-1);
			}
			
		}

		public bool IsMouseOnBounds()
		{		
			Vector2 coord = PixelCoordFromMousePos ();

			if (coord.x < 0 || coord.x > 1 || coord.y < 0 || coord.y > 1)
				return false;

			return true;
			
		}

		// Update is called once per frame
		void Update () {
			if ( Input.GetMouseButtonDown(0) && IsMouseOnBounds () ) {
				action.DoAction();
			}
		}
	}


}