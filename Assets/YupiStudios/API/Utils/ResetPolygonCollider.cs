using UnityEngine;
using System.Collections;


namespace YupiStudios.API.Utils
{

	public class ResetPolygonCollider : MonoBehaviour {

		// Use this for initialization
		void Start () {
			PolygonCollider2D col = (PolygonCollider2D)GetComponent<Collider2D>();
			Vector2 [] points = new Vector2[3];
			Vector2 [] pointsPath1 = new Vector2[3];
			points [0] = new Vector2 (0,0);
			points [1] = new Vector2 (1,1);
			points [2] = new Vector2 (2,0);
			col.points = points;

			pointsPath1 [0] = points [0];
			pointsPath1 [1] = points [1];
			pointsPath1 [2] = points [2];
			col.pathCount = 1;

			col.SetPath (0, pointsPath1);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}
