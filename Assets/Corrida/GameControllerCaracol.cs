using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameControllerCaracol : MonoBehaviour {
	public GameObject leftCorners;
	public GameObject rightCorners;
	public Animator speedBar;
	public Animator speedCursor;
	public float speed;
	public float maxspeed = 5;
	public GameObject PlayerControlled;
	public int laps;
	public int lapsMax;
	public bool halfwayPoint;
    public GameObject endingPanel;
	public GameObject victoryPanel;
	public int pistasLiberadas;
	public int jogadorID;
	public Button pistaButton2;
	public Button pistaButton3;
	public GameObject panel;

	//Audios
	public AudioClip menuClick;
	public AudioClip derrapar;
	public AudioClip[] andar;
	private float andarCd = 0.6f;
	public AudioClip voltaCompleta;
	public AudioClip fimCorrida;
	public AudioClip semaforo;

	public AudioClip bgMusic;


	public GameObject animStart;
	public GameObject animEnd;

	void Awake(){		
		if (Application.loadedLevelName == "SeleçãoPistas") {
			Time.timeScale = 1;
			//PlayerPrefs.SetInt("jogadorID",1);
		} else {
			Time.timeScale = 0;
			animStart.GetComponent<Animator> ().SetTrigger ("Play");

			jogadorID = PlayerPrefs.GetInt ("jogadorID") == 0 ? 1 : PlayerPrefs.GetInt ("jogadorID");

			if (jogadorID == 1) {
				PlayerControlled = GameObject.Find ("lunacol_256x256").gameObject;
				Camera.main.GetComponent<CameraFollow> ().setTarget (GameObject.Find ("lunacol_256x256").transform);
			} else if (jogadorID == 2) {
				PlayerControlled = GameObject.Find ("jupitercol_256x256").gameObject;
				Camera.main.GetComponent<CameraFollow> ().setTarget (GameObject.Find ("jupitercol_256x256").transform);
			} else if (jogadorID == 3) {
				PlayerControlled = GameObject.Find ("cludiocol_256x256").gameObject;
				Camera.main.GetComponent<CameraFollow> ().setTarget (GameObject.Find ("cludiocol_256x256").transform);
			}

		}

		pistasLiberadas = PlayerPrefs.GetInt ("qtdPistas");
		this.gameObject.SetActive (true);
		if(pistasLiberadas <= 1){
			if(pistaButton2 != null && pistaButton2 != null ){
			pistaButton2.interactable = false;
			pistaButton3.interactable = false;
			}
		}else if (pistasLiberadas == 2) {
			if(pistaButton2 != null && pistaButton2 != null ){
			pistaButton2.interactable = true;
			pistaButton3.interactable = false;
			}
		} else if (pistasLiberadas == 3) {
			if(pistaButton2 != null && pistaButton2 != null ){
			pistaButton2.interactable = true;
			pistaButton3.interactable = true;
			}
		} 



	}
	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt("qtdPistas", 1);

		gameObject.GetComponent<AudioSource> ().clip = bgMusic;
		gameObject.GetComponent<AudioSource> ().loop = true;
		gameObject.GetComponent<AudioSource> ().Play ();

		if (Application.loadedLevelName != "SeleçãoPistas") {

			PlayerControlled.GetComponent<Waypoints> ().isTheControlled = true;
		} 

		laps = 0;
//		if (PlayerControlled.GetComponent<Waypoints>().players == Waypoints.Characters.Claudio) {
//			
//		}

		//FixingCorners ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (speed);

		if (PlayerControlled != null) {
			if (PlayerControlled.GetComponent<Waypoints> ().lento) {
				maxspeed = 1;
			} else {
				maxspeed = 5;
			}

			if (PlayerControlled.GetComponent<Waypoints> ().rapido) {
				maxspeed = 9;
			} else {
				maxspeed = 5;
			}
		}
	
		if (this.speed > maxspeed)
			this.speed = maxspeed;

		if(this.speed>0)
		this.speed = speed - Time.deltaTime * 1.5f;

		if (GameObject.Find ("Voltas") != null) {
			GameObject.Find ("Voltas").GetComponent<Text> ().text = laps + "/" + lapsMax;
		}

        if (PlayerControlled != null && Time.timeScale>0)
        {

            PlayerControlled.GetComponent<Waypoints>().playerMark.SetActive(true);

            if (PlayerControlled.GetComponent<Waypoints>().currentWaypoint.halfway)
                halfwayPoint = true;

            if (PlayerControlled.GetComponent<Waypoints>().currentWaypoint.startway && halfwayPoint)
            {
                halfwayPoint = false;
				PlayAudio(voltaCompleta);
                laps++;
            }

            if (laps >= lapsMax) { 
				if(PlayerControlled.GetComponent<Waypoints>().Position == 1 || PlayerControlled.GetComponent<Waypoints>().Position == 2 ){
					if(pistasLiberadas<2){
					PlayerPrefs.SetInt("qtdPistas", 2);
					}else if(pistasLiberadas >= 2){
						PlayerPrefs.SetInt("qtdPistas", 3);
					}
					animEnd.SetActive(true);
				}else{

					endingPanel.SetActive(true);
				}
				PlayAudio(fimCorrida);
                SetTimeScale(0);
             }
			speedBar.SetFloat ("Blend", this.speed);
			PlayerControlled.GetComponent<Waypoints> ().speed = this.speed;

			//noise generator
			andarCd = Time.deltaTime + andarCd;
			if (andarCd >= 1.5f) {

				gameObject.GetComponent<AudioSource> ().PlayOneShot (andar[Random.Range(0,2)],0.3f);

				andarCd = 0;
			}

		}
	}

	public void TouchInput(){
		speedCursor.SetTrigger ("Move");
		if(this.speed<maxspeed)
		this.speed = speed + Time.deltaTime * 120;
	}

	public void FixingCorners(){

		Vector3 l =  Camera.main.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.01f, 1.0f, 0.25f));
		Vector3 r =  Camera.main.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.99f, 1.0f, 0.25f));

		leftCorners.transform.position = new Vector3(l.x,0,leftCorners.transform.position.z);
		rightCorners.transform.position = new Vector3(r.x,0,rightCorners.transform.position.z);

	}

	public void SetPlayer(int player){
		PlayerPrefs.SetInt("jogadorID", player);
		//this.PlayerControlled = player;
	}

	public void SetTimeScale(float x){
		
		Time.timeScale = x;
	}


	public void StartRace(){
		if (PlayerControlled == null) {
			PlayerControlled = GameObject.Find("lunacol_256x256").gameObject;
			Camera.main.GetComponent<CameraFollow>().setTarget(GameObject.Find("lunacol_256x256").transform);
		}
		panel.SetActive (false);
		animStart.GetComponent<Animator> ().SetTrigger ("Play");
	}

	public void CallScene(string x){
		
		if(x == "exit"){
			Time.timeScale = 1;
			Application.LoadLevel("NewMenu");
		}else{
			Application.LoadLevel(x);
        }

	}

	public void PlayAudio(AudioClip x){
		gameObject.GetComponent<AudioSource> ().PlayOneShot (x);
	}



}
