using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunecoController : MonoBehaviour {

	private static BunecoController instance;
	public static BunecoController Instance {
		get{
			if (instance == null)
				instance = GameObject.FindObjectOfType<BunecoController> ();
			return instance;
		}
	}

	public BunecoAcessoriosController acessóriosController;
	public BunecoCorpoController corpoController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
