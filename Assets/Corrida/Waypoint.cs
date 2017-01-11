using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public float waitSeconds = 0;
	public float speedOut = 0;
	public bool flipSprite = false;
	public bool upSprite;
	public bool downSprite;
	public bool halfway;
	public bool startway;
	public int racepos = 0;

	public GameObject fruta;
	public GameObject oleo;
	private float itemCD;

	void Update()
	{
		itemCD += Time.deltaTime;
		if (itemCD >= Random.Range(10,20)) {
			int x = Random.Range(0,20);
			if(x==1){
			 GameObject gobj = (GameObject)Instantiate(fruta,this.transform.position, Quaternion.identity);
				Destroy(gobj,20);
			}
			if(x==2){
			GameObject gobj = (GameObject) Instantiate(oleo,this.transform.position, Quaternion.identity);
			Destroy(gobj,20);
			}
			itemCD = 0;
		}
	}

	public int GetWaypoint(){
		racepos = racepos + 1;

		if (racepos >= 4) {
			racepos = 1;
		}
		return racepos;
	}
}
