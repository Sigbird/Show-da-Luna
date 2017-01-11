using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour {

	public GameObject leftTree;
	public GameObject rightTree;
	public GameObject leftBranch;
	public GameObject rightBranch;
	public GameObject branches;
	public GameObject leftAirplane;
	public GameObject rightAirplane;
	public GameObject loopAirplane;
	public GameObject extraFeather;


	public Transform TopLeftTree;
	public Transform TopRightTree;
	public Transform BottonLeftTree;
	public Transform BottonRightTree;
	public Transform TopLeftTreeLast;
	public Transform TopRightTreeLast;
	public Transform BottonLeftTreeLast;
	public Transform BottonRightTreeLast;
	private bool onscreenfromTop = false;
	private bool onscreenfromBotton = true;

	public GameObject[] ArvoresLeft;
	public GameObject[] ArvoresRight;

	private float CurrentHeightUp;
	private float CurrentHeightDown;

	public Vector3 l ;
	public Vector3 r ;

	// Use this for initialization
	void Start () {
		CurrentHeightUp = this.transform.position.y;
		CurrentHeightDown = this.transform.position.y;

		l =  Camera.main.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.03f, 1.0f, 0.25f));
		r =  Camera.main.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.98f, 1.0f, 0.25f));

		foreach (GameObject t in ArvoresLeft){
			t.transform.position = new Vector3(l.x,t.transform.position.y,t.transform.position.z);
		}

		foreach (GameObject t in ArvoresRight){
			t.transform.position = new Vector3(r.x,t.transform.position.y,t.transform.position.z);
		}

		GameObject LT = GameObject.Find ("LeftTree");
		GameObject RT = GameObject.Find("RightTree");

		if (LT != null && RT != null) {
			LT.transform.position = new Vector3(l.x,LT.transform.position.y,LT.transform.position.z);
			RT.transform.position = new Vector3(r.x,RT.transform.position.y,RT.transform.position.z);
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		//CurrentHeight = CurrentHeight + this.transform.position.y;
//			Debug.Log("Up: " + CurrentHeightUp);
//			Debug.Log("Down: " + CurrentHeightDown);
//		Debug.Log (CurrentHeightUp);

		//RESPAWN DE ELEMENTOS PARA CIMA
		if (this.transform.position.y <= (CurrentHeightUp - 5f)) {
			CurrentHeightUp = Mathf.Round(this.transform.position.y);

			ArvoresLeft[ArvoresLeft.Length-1].transform.position = new Vector3(l.x,ArvoresLeft[0].transform.position.y+5f,ArvoresLeft[0].transform.position.z);
			ArvoresRight[ArvoresRight.Length-1].transform.position = new Vector3(r.x,ArvoresRight[0].transform.position.y+5f,ArvoresRight[0].transform.position.z);

			GameObject arvorelefttemp = ArvoresLeft[ArvoresLeft.Length-1];
			GameObject arvorerighttemp = ArvoresRight[ArvoresRight.Length-1];

			ArvoresLeft.SetValue(ArvoresLeft[3],4);
			ArvoresLeft.SetValue(ArvoresLeft[2],3);
			ArvoresLeft.SetValue(ArvoresLeft[1],2);
			ArvoresLeft.SetValue(ArvoresLeft[0],1);
			ArvoresLeft.SetValue(arvorelefttemp,0);

			ArvoresRight.SetValue(ArvoresRight[3],4);
			ArvoresRight.SetValue(ArvoresRight[2],3);
			ArvoresRight.SetValue(ArvoresRight[1],2);
			ArvoresRight.SetValue(ArvoresRight[0],1);
			ArvoresRight.SetValue(arvorerighttemp,0);


			//RESPAWN DE GALHOS E ELEMENTOS DO CENARIO PARA CIMA
			//ALEATORIZADOR
			int x = Random.Range(0,3);

			//EVENTOS
			if (x == 1){
				GameObject a = (GameObject) Instantiate(leftBranch, arvorelefttemp.transform.position , Quaternion.identity);
				a.transform.parent = branches.transform;
				if(Random.Range(0,2) == 0){
					GameObject b = (GameObject) Instantiate(rightAirplane, arvorerighttemp.transform.position , Quaternion.identity);
					b.transform.parent = this.transform;
				}
			}else if (x != 1 && x != 2 && Random.Range(0,10) == 0){
				GameObject b = (GameObject) Instantiate(extraFeather, new Vector3(0,arvorerighttemp.transform.position.y,arvorerighttemp.transform.position.z) , Quaternion.identity);
				b.transform.parent = this.transform;
			}

			if (x == 2){
				GameObject a = (GameObject) Instantiate(rightBranch, arvorerighttemp.transform.position , Quaternion.identity);
				a.transform.parent = branches.transform;
				if(Random.Range(0,2) == 0){
					GameObject b = (GameObject) Instantiate(leftAirplane, arvorelefttemp.transform.position , Quaternion.identity);
					b.transform.parent = this.transform;
				}
			}else if (x != 1 && x != 2 && Random.Range(0,10) == 0){
				GameObject b = (GameObject) Instantiate(extraFeather, new Vector3(0,arvorerighttemp.transform.position.y,arvorerighttemp.transform.position.z) , Quaternion.identity);
				b.transform.parent = this.transform;
			}

			if (x == 0 && GameController.Apple.transform.position.y < -5 && GameController.score > 1000){
				if(Random.Range(0,2) == 0){
					StartCoroutine(LoopAirplaneCallerUp(arvorelefttemp));
				}else{
					StartCoroutine(GameController.AppleFall());
				}
			}



			//INCREMENTADOR DE SCORE
			GameController.score = GameController.score + 100;

		}


		//RESPAWN DE ELEMENTOS PARA BAIXO
		if (this.transform.position.y >= (CurrentHeightUp + 5f)) {
			CurrentHeightUp = Mathf.Round(this.transform.position.y);

			ArvoresLeft[0].transform.position = new Vector3(ArvoresLeft[ArvoresRight.Length-1].transform.position.x,ArvoresLeft[ArvoresRight.Length-1].transform.position.y-5f,ArvoresLeft[ArvoresRight.Length-1].transform.position.z);
			ArvoresRight[0].transform.position = new Vector3(ArvoresRight[ArvoresRight.Length-1].transform.position.x,ArvoresRight[ArvoresRight.Length-1].transform.position.y-5f,ArvoresRight[ArvoresRight.Length-1].transform.position.z);
			
			GameObject arvorelefttemp = ArvoresLeft[0];
			GameObject arvorerighttemp = ArvoresRight[0];
			
			ArvoresLeft.SetValue(ArvoresLeft[1],0);
			ArvoresLeft.SetValue(ArvoresLeft[2],1);
			ArvoresLeft.SetValue(ArvoresLeft[3],2);
			ArvoresLeft.SetValue(ArvoresLeft[4],3);
			ArvoresLeft.SetValue(arvorelefttemp,4);
			
			ArvoresRight.SetValue(ArvoresRight[1],0);
			ArvoresRight.SetValue(ArvoresRight[2],1);
			ArvoresRight.SetValue(ArvoresRight[3],2);
			ArvoresRight.SetValue(ArvoresRight[4],3);
			ArvoresRight.SetValue(arvorerighttemp,4);

			//RESPAWN DE GALHOS E ELEMENTOS DO CENARIO PARA BAIXO
			int x = Random.Range(0,3);
//			Debug.Log(x);
			if (x == 1){
				GameObject a = (GameObject) Instantiate(leftBranch, arvorelefttemp.transform.position , Quaternion.identity);
				a.transform.parent = branches.transform;
				if(Random.Range(0,2) == 0){
					GameObject b = (GameObject) Instantiate(rightAirplane, arvorerighttemp.transform.position , Quaternion.identity);
					b.transform.parent = this.transform;
				}
			}else if (Random.Range(0,5) == 0){
				GameObject b = (GameObject) Instantiate(extraFeather, new Vector3(0,arvorerighttemp.transform.position.y,arvorerighttemp.transform.position.z) , Quaternion.identity);
				b.transform.parent = this.transform;
			}
			
			if (x == 2){
				GameObject a = (GameObject) Instantiate(rightBranch, arvorerighttemp.transform.position , Quaternion.identity);
				a.transform.parent = branches.transform;
				if(Random.Range(0,2) == 0){
					GameObject b = (GameObject) Instantiate(leftAirplane, arvorelefttemp.transform.position , Quaternion.identity);
					b.transform.parent = this.transform;
				}
			}else if (Random.Range(0,10) == 0){
				GameObject b = (GameObject) Instantiate(extraFeather, new Vector3(0,arvorelefttemp.transform.position.y,arvorelefttemp.transform.position.z) , Quaternion.identity);
				b.transform.parent = this.transform;
			}

			if (x == 0 && GameController.Apple.transform.position.y < -5){
				if(Random.Range(0,1) == 0){
					StartCoroutine(LoopAirplaneCallerDown(arvorelefttemp));
				}else{
					StartCoroutine(GameController.AppleFall());
				}
			}

			//DECREMENTADOR DE SCORE
			if(GameController.score>= 100)
			GameController.score = GameController.score - 100;


		}

		//DESTRUIDOR DE ARVORES
		GameObject[] LT = GameObject.FindGameObjectsWithTag ("Branch");
		//GameObject[] RT = GameObject.FindGameObjectsWithTag ("RightTree");

		foreach (GameObject t in LT) {
		
			if (t.transform.position.y < 15 && t.transform.position.y > -15) {
				//do nothing
			} else {
				Destroy (t.gameObject);
			}
		}

//		foreach (GameObject t in LT) {
//
//			if (t.transform.position.y < 9 && t.transform.position.y > -9) {
//				//do nothing
//			} else {
//				Destroy (t.gameObject);
//			}
//		}

//		Debug.Log (CurrentHeight);
	}

	IEnumerator LoopAirplaneCallerDown(GameObject obj){
		GameController.WarningSign.transform.position = new Vector3(-6, 0, 0);
		yield return new WaitForSeconds (1);
		GameController.WarningSign.transform.position = new Vector3(-10, 0, 0);
		GameObject b = (GameObject) Instantiate(loopAirplane, new Vector3(0,obj.transform.position.y+10,obj.transform.position.z) , Quaternion.identity);
		b.transform.parent = this.transform;
	}

	IEnumerator LoopAirplaneCallerUp(GameObject obj){
		GameController.WarningSign.transform.position = new Vector3(-6, 0, 0);
		yield return new WaitForSeconds (1);
		GameController.WarningSign.transform.position = new Vector3(-10, 0, 0);
		GameObject b = (GameObject) Instantiate(loopAirplane, new Vector3(0,obj.transform.position.y-5,obj.transform.position.z) , Quaternion.identity);
		b.transform.parent = this.transform;
	}
}
