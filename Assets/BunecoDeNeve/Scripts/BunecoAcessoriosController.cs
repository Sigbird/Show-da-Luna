using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunecoAcessoriosController : MonoBehaviour {
	private int step;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Revert(){
		step--;
	}

	public void SelectItem(int x){
		step++;
	}
}
