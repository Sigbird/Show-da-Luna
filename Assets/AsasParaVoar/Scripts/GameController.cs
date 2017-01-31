using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameObject Apple;
	public GameObject Bird;
	public GameObject BirdSprite;
	public GameObject Cenario;
	public GameObject BackGround;
	public static GameObject WarningSign;
	public string LastMove;
	public static bool BirdGrounded;
	public Image[] Featers;
	public Sprite EmptyFeater;
	public Sprite FullFeater;
	public static int Vida;
	public static int score;
	public static float ScreenSpeed;
	public Text endingScore;
	public GameObject endingPanel;
	public static int LastApplePos;

	public static AudioSource audio;
	public AudioClip Musica;
	public AudioClip Flap;
	public AudioClip Dano;


	public Button jupiterButton;
	public Button lunaButton;
	public Button claudioButton;

	private Camera mainCamera;

	public float FlySpeed = 3f;
	public float MaxFlySpeedY = 7f;
	public float MaxGravitySpeed = 4f;
    public float BorderOffset = 2f;
	public float VerticalOffset = 1f;

	private float bottom;
	private float top;
	private float left;
	private float right;

    private Rigidbody2D birdRb;
	private Animator birdAnim;
	private SpriteRenderer birdRender;
	private Rigidbody2D cenarioRb;
	private Text scoreText;
	private static Rigidbody2D appleRb;

	public enum birds{
		
		Jupiter,
		Luna,
		Claudio,
		
	}

	public birds ActiveBird;


	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = Musica;
		audio.Play ();
		audio.volume = 1;
		WarningSign = GameObject.Find ("Exclamation");
		Apple = GameObject.Find ("Apple");
		appleRb = Apple.GetComponent<Rigidbody2D>();

		GameController.Vida = 3;
		PauseGame (false);
		GameController.score = 0;
		StartCoroutine ("BlinkWarningSign");

        birdRb = Bird.GetComponent<Rigidbody2D>();
		birdAnim = BirdSprite.GetComponent<Animator>();
		birdRender = BirdSprite.GetComponent<SpriteRenderer>();
		cenarioRb = Cenario.GetComponent<Rigidbody2D>();
		scoreText = GameObject.Find ("Scoretext").GetComponent<Text> ();

        mainCamera = Camera.main;

		//LIMITES DO VIEWPORT
		bottom = mainCamera.ViewportToWorldPoint(new Vector3(0,0,mainCamera.transform.position.z)).y + VerticalOffset;
        top = mainCamera.ViewportToWorldPoint(new Vector3(1,1,mainCamera.transform.position.z)).y - VerticalOffset;
		left = mainCamera.ViewportToWorldPoint(new Vector3(0,0,mainCamera.transform.position.z)).x + BorderOffset;
		right = mainCamera.ViewportToWorldPoint(new Vector3(1,1,mainCamera.transform.position.z)).x - BorderOffset;
        Debug.Log("left:" + left);
        Debug.Log("right:" + right);

	}
		
	void FixedUpdate(){

//		if (transform.position.y <= bottom) {			
//			Bird.GetComponent<Rigidbody2D>().velocity = new Vector2(Bird.GetComponent<Rigidbody2D>().velocity.x, FlySpeed);
//			//HitAction();
//			return;
//		}
//		if (transform.position.y >= top) {			
//			Bird.GetComponent<Rigidbody2D>().velocity = new Vector2(Bird.GetComponent<Rigidbody2D>().velocity.x, -FlySpeed);
//			//HitAction();
//			return;
//		}

		//Loop vertical do cenario (2.5 ref)
		if (Bird.transform.position.y >= 0f) {
			cenarioRb.MovePosition((Vector2) new Vector2(0,Cenario.transform.position.y) + (-Vector2.up * Time.deltaTime * Bird.transform.position.y ));
			//Cenario.transform.Translate (-Vector2.up * Time.deltaTime * BirdSprite.transform.position.y);
			if (BackGround.transform.position.y > -30) {
				BackGround.transform.Translate (-Vector2.up * Time.deltaTime * (Bird.transform.position.y / 10.5f));
			}
		}

		if (Bird.transform.position.y <= -0f) {
			cenarioRb.MovePosition((Vector2) new Vector2(0,Cenario.transform.position.y) + (-Vector2.up * Time.deltaTime * Bird.transform.position.y));
			//Cenario.transform.Translate (-Vector2.up * Time.deltaTime * BirdSprite.transform.position.y);
			if (BackGround.transform.position.y < 1.7f) {
				BackGround.transform.Translate (-Vector2.up * Time.deltaTime * (Bird.transform.position.y / 10.5f));
			}
		}

		//Queda de Maça
		if(Apple.transform.position.y > -6) {
			appleRb.MovePosition((Vector2) Apple.transform.position + (-Vector2.up * Time.deltaTime * 10));
			//Apple.transform.Translate (-Vector2.up * Time.deltaTime * 10);	
		}


		if (Bird.transform.position.x <= left) {			
			birdRb.velocity = new Vector2(FlySpeed, birdRb.velocity.y);
			birdRender.flipX = true;
			//HitAction();
			return;
		}
		if (Bird.transform.position.x >= right) {			
			birdRb.velocity = new Vector2(-FlySpeed, birdRb.velocity.y);
			birdRender.flipX = false;
			//HitAction();
			return;
		}

		if (Bird.transform.position.y >= top) {
			birdRb.velocity = new Vector2(birdRb.velocity.x, 0);
			return;
		}
		if (Bird.transform.position.y <= bottom) {
			birdRb.velocity = new Vector2(0, FlySpeed);
			FlapAction();
			return;
		}
			
		birdRb.velocity = new Vector2(birdRb.velocity.x, Mathf.Clamp(birdRb.velocity.y, -MaxGravitySpeed, MaxFlySpeedY));
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString ();
		endingScore.text = score.ToString ();

		//Debug de Teclado
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			FlyRight ();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			FlyLeft ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			GameController.AppleFall ();
		}
		//Check de Vida
		UpdateVida ();

		//Passaro Segue seu controller
		//BirdSprite.transform.position = Vector2.MoveTowards (BirdSprite.transform.position, Bird.transform.position, 8 * Time.deltaTime);
        //BirdSprite.transform.position = Bird.transform.position;

		//Loop de laterais do cenario
//		if (BirdSprite.transform.position.x >= 6f && Bird.transform.position.x >= 6f) {
//			BirdSprite.transform.position = new Vector3 (6, BirdSprite.transform.position.y, BirdSprite.transform.position.z);
//			Bird.transform.position = new Vector3 (6, Bird.transform.position.y, Bird.transform.position.z);
//			Debug.Log("entrou");
//		}
//
//		if (BirdSprite.transform.position.x <= -6f && Bird.transform.position.x <= -6f) {
//			BirdSprite.transform.position = new Vector3 (-6, BirdSprite.transform.position.y, BirdSprite.transform.position.z);
//			Bird.transform.position = new Vector3 (-6, Bird.transform.position.y, Bird.transform.position.z);
//		}




		//Queda
//		if (BirdGrounded == false && BirdSprite.transform.position.y >= -3.5f && Bird.transform.position.y >= -3.5f) {
//			Bird.transform.Translate (-Vector2.up * Time.deltaTime * 1.5f);
//			if (LastMove == "Left") {
//				Bird.transform.Translate (-Vector2.right * Time.deltaTime * 0.7f);
//			}
//
//			if (LastMove == "Right") {
//				Bird.transform.Translate (Vector2.right * Time.deltaTime * 0.7f);
//			}
//		}



	}

	public void FlyLeft(){
		//Bird.rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, 0);
//		Bird.rigidbody2D.AddForce (Vector2.up * 50);
//		Bird.rigidbody2D.AddForce (-Vector2.right * 10);

        //if (BirdSprite.transform.position.y <= 3.5f && Bird.transform.position.y <= 3.5f ) {
//			BirdSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
//			Bird.transform.Translate (Vector2.up * 1);
//			Bird.transform.Translate (-Vector2.right * 0.2f);
//			BirdSprite.GetComponent<Animator>().SetTrigger("Flap");
//			audio.PlayOneShot(Flap,0.5f);
			birdRb.velocity = new Vector2(-FlySpeed, getFlySpeedY());
			birdRender.flipX = false;
			FlapAction();
			LastMove = "Left";
		//}
	}

	public void FlyRight(){
		//Bird.rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, 0);
//		Bird.rigidbody2D.AddForce (Vector2.up * 50);
//		Bird.rigidbody2D.AddForce (Vector2.right * 10);

        //		if (BirdSprite.transform.position.y <= 3.5f && Bird.transform.position.y <= 3.5f ) {
//			BirdSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
//			Bird.transform.Translate (Vector2.up * 1);
//			Bird.transform.Translate (Vector2.right * 0.2f);
//			BirdSprite.GetComponent<Animator>().SetTrigger("Flap");
//			audio.PlayOneShot(Flap,0.5f);
			birdRb.velocity = new Vector2(FlySpeed, getFlySpeedY());
			birdRender.flipX = true;
			FlapAction();
			LastMove = "Right";
//		}
	}

	private float getFlySpeedY() {				
		return Mathf.Clamp(birdRb.velocity.y + FlySpeed, 0, MaxFlySpeedY);
	}

	private void FlapAction() {
		birdAnim.SetTrigger("Flap");
		audio.PlayOneShot(Flap,0.5f);
	}

	public void UpdateVida(){
//		Debug.Log (Vida);
		switch (Vida)
		{
		case 3:
			Featers[0].sprite = FullFeater;
			Featers[1].sprite = FullFeater;
			Featers[2].sprite = FullFeater;
			break;
		case 2:
			Featers[0].sprite = EmptyFeater;
			Featers[1].sprite = FullFeater;
			Featers[2].sprite = FullFeater;
			break;
		case 1:
			Featers[0].sprite = EmptyFeater;
			Featers[1].sprite = EmptyFeater;
			Featers[2].sprite = FullFeater;
			break;
		default:
			if(endingPanel.activeSelf){
				//
			}else{
				PauseGame(true);
				endingPanel.SetActive(true);
			}
			break;
		}
	}

	public void CallScene(string txt){

		if (txt == "exit") {
			PauseGame(false);
			Application.LoadLevel("NewMenu");
		} else {
			Application.LoadLevel(txt);
		}

	}

	public void PauseGame(bool x){
		if (x) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public static IEnumerator AppleFall (){
		int x = Random.Range(0,3);
		//int last = 3;
		switch (x) {
		case 0:
			if(x!=LastApplePos){
				WarningSign.transform.position = new Vector3(-3.5f, 4, 0);
				yield return new WaitForSeconds(1);
				WarningSign.transform.position = new Vector3(-3.5f, 8, 0);
				appleRb.MovePosition((Vector2) new Vector3(-3.5f, 8, 0));
				//Apple.transform.position = new Vector3(-3.5f, 8, 0);
				LastApplePos = x;
			}else{ AppleFall();}
			break;
		case 1:
			if(x!=LastApplePos){
				WarningSign.transform.position = new Vector3(0, 4, 0);
				yield return new WaitForSeconds(1);
				WarningSign.transform.position = new Vector3(-3.5f, 8, 0);
				appleRb.MovePosition((Vector2) new Vector3(0, 8, 0));
				//Apple.transform.position = new Vector3(0, 8, 0);
				LastApplePos = x;
			}else{ AppleFall();}
			break;
		case 2:
			if(x!=LastApplePos){
				WarningSign.transform.position = new Vector3(3.5f, 4, 0);
				yield return new WaitForSeconds(1);
				WarningSign.transform.position = new Vector3(-3.5f, 8, 0);
				appleRb.MovePosition((Vector2) new Vector3(3.5f, 8, 0));
				//Apple.transform.position = new Vector3(3.5f, 8, 0);
				LastApplePos = x;
			}else{ AppleFall();}
			break;
		default:
			break;
		}
	}

	IEnumerator BlinkWarningSign(){
		WarningSign.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds(0.2f);
		WarningSign.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds(0.2f);
		StartCoroutine ("BlinkWarningSign");
	}

	public void ChangePlayer(int x){

		switch(x)
		{
		case 1:
			jupiterButton.GetComponent<Animator>().SetTrigger("Disabled");
			birdAnim.SetInteger("Char",0);
			ActiveBird = birds.Jupiter;
			break;
		case 2:
			lunaButton.GetComponent<Animator>().SetTrigger("Disabled");
			birdAnim.SetInteger("Char",1);
			ActiveBird = birds.Luna;
			break;
		case 3:
			claudioButton.GetComponent<Animator>().SetTrigger("Disabled");
			birdAnim.SetInteger("Char",2);
			ActiveBird = birds.Claudio;
			break;
		default:
			print ("Incorrect Bird.");
			break;
		}
		
	}
}
	