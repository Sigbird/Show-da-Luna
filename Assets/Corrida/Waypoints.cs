using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {

	public enum Characters {Luna, Jupiter, Claudio};

	public Characters players;
	public int Position;
	public Waypoint[] wayPoints;

	public bool isCircular;
	// Always true at the beginning because the moving object will always move towards the first waypoint
	public bool inReverse = true;
	
	public Waypoint currentWaypoint;
	private int currentIndex   = 0;
	private bool isWaiting     = false;
	private float speedStorage = 0;
	public Rigidbody2D rb;
	private SpriteRenderer sprite;
	public GameObject playerMark;
	public int positioncount;
	public Waypoints Adv1;
	public Waypoints Adv2;
	public GameObject goo;
	public GameObject horizontalGoo;
	private float gooCd;
	public bool isTheControlled;
	private float lastDistance;
	private float driftRate = 0;
	private bool derrapou;
	public bool lento;
	public bool rapido;
	private float effectCD1;
	private float effectCD2;


	[HideInInspector] 
	public float speed = 2;

	
	
	/**
     * Initialisation
     * 
     */
	void Start () {
		//Characters Chars;


		rb = GetComponent<Rigidbody2D>();
		if(wayPoints.Length > 0) {
			currentWaypoint = wayPoints[0];
		}
		sprite = GetComponent<SpriteRenderer>();
	}
	
	
	
	/**
     * Update is called once per frame
     * 
     */
	void Update()
	{

		if (isTheControlled == false && rapido == false && lento == false) {
			this.speed = Random.Range (3.5f, 4.5f);
		}

		if (lento) {
			effectCD1 += Time.deltaTime;
			this.speed = 1;
			if(effectCD1 > 3){
				lento = false;
			}
		}

		if (rapido) {
			effectCD2 += Time.deltaTime;
			this.speed = 10;
			if(effectCD2 > 3){
				rapido = false;
			}
		}

		gooCd = Time.deltaTime + gooCd;
		if (gooCd >= 1f) {
			if(currentWaypoint.upSprite){
				Instantiate(goo,new Vector2(this.transform.position.x,this.transform.position.y+0.2f),Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360f)));
			}else if(currentWaypoint.downSprite){
				Instantiate(goo,new Vector2(this.transform.position.x,this.transform.position.y),Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 360f)));
			}else{
				Instantiate(horizontalGoo,new Vector2(this.transform.position.x,this.transform.position.y+0.2f),Quaternion.identity);
			}
			gooCd = 0;
		}


//		if (positioncount > Adv1.positioncount && positioncount > Adv2.positioncount) {
//			Position = 1;
//		}
//
//		if (positioncount > Adv1.positioncount && positioncount <= Adv2.positioncount) {
//			Position = 2;
//		}
//
//		if (positioncount <= Adv1.positioncount && positioncount > Adv2.positioncount) {
//			Position = 2;
//		}
//
//		if (positioncount < Adv1.positioncount && positioncount < Adv2.positioncount) {
//			Position = 3;
//		}

		playerMark.gameObject.transform.GetChild (0).GetComponent<PositionScript> ().pos = Position;

		if(currentWaypoint != null && !isWaiting) {
			MoveTowardsWaypoint();
			if(currentWaypoint.flipSprite){
				switch(players)
				{
				case Characters.Luna:
					sprite.sortingOrder = 3;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 3;
					break;
				case Characters.Jupiter:
					sprite.sortingOrder = 2;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 2;
					break;
				case Characters.Claudio:
					sprite.sortingOrder = 1;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 1;
					break;
				default:
					print ("Incorrect Character.");
					break;
				}
				transform.localRotation = Quaternion.Euler(0, 180, 0);
				playerMark.gameObject.transform.GetChild (0).gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
			}else{
				switch(players)
				{
				case Characters.Luna:
					sprite.sortingOrder = 1;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 1;
					break;
				case Characters.Jupiter:
					sprite.sortingOrder = 2;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 2;
					break;
				case Characters.Claudio:
					sprite.sortingOrder = 3;
					playerMark.GetComponent<SpriteRenderer>().sortingOrder = 3;
					break;
				default:
					print ("Incorrect Character.");
					break;
				}
				transform.localRotation = Quaternion.Euler(0, 0, 0);
				playerMark.gameObject.transform.GetChild (0).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

			}

			gameObject.GetComponent<Animator>().SetBool("VerticalUp",currentWaypoint.upSprite);
			gameObject.GetComponent<Animator>().SetBool("VerticalDown",currentWaypoint.downSprite);
		}
	}
	
	
	
	/**
     * Pause the mover
     * 
     */
	void Pause()
	{
		isWaiting = !isWaiting;
	}
	
	
	
	/**
     * Move the object towards the selected waypoint
     * 
     */
	private void MoveTowardsWaypoint()
	{
		// Get the moving objects current position
		Vector3 currentPosition = this.transform.position;
		
		// Get the target waypoints position
		Vector3 targetPosition = currentWaypoint.transform.position;
		
		// If the moving object isn't that close to the waypoint
		if(Vector3.Distance(currentPosition, targetPosition) > .5f) {
			//Drift Controller
//			if(this.isTheControlled){
//			if (lastDistance < Vector3.Distance(currentPosition, targetPosition)){
//				this.driftRate += Time.deltaTime;
//			}
//			if (driftRate >= 0.25f && derrapou == false){
//				Debug.Log("derrapou");
//				GameObject.Find("GameController").GetComponent<GameControllerCaracol>().PlayAudio(GameObject.Find("GameController").GetComponent<GameControllerCaracol>().derrapar);
//				StartCoroutine(Flash(0.1f));
//				derrapou = true;
//				driftRate = 0;
//			}
//			}

			// Get the direction and normalize
			if(derrapou == false){
				Vector3 directionOfTravel = targetPosition - currentPosition;
				directionOfTravel.Normalize();
				rb.AddForce(directionOfTravel * speed * Time.deltaTime);
			}else{
				Vector3 directionOfTravel = targetPosition - currentPosition;
				directionOfTravel.Normalize();
				rb.AddForce(directionOfTravel * 3 * Time.deltaTime);
			}

			
			//scale the movement on each axis by the directionOfTravel vector components

//			this.transform.Translate(
//				directionOfTravel.x * speed * Time.deltaTime,
//				directionOfTravel.y * speed * Time.deltaTime,
//				directionOfTravel.z * speed * Time.deltaTime,
//				Space.World
//				);
			lastDistance = Vector3.Distance(currentPosition, targetPosition);

		} else {

			derrapou = false;
			// If the waypoint has a pause amount then wait a bit
			if(currentWaypoint.waitSeconds > 0) {
				Pause();
				Invoke("Pause", currentWaypoint.waitSeconds);
			}
			
			// If the current waypoint has a speed change then change to it
			if(currentWaypoint.speedOut > 0) {
				speedStorage = speed;
				speed = currentWaypoint.speedOut;
			} else if(speedStorage != 0) {
				speed = speedStorage;
				speedStorage = 0;
			}

			NextWaypoint();
		}
	}
	
	
	
	/**
     * Work out what the next waypoint is going to be
     * 
     */
	private void NextWaypoint()
	{
		if(isCircular) {
			
			if(!inReverse) {
				currentIndex = (currentIndex+1 >= wayPoints.Length) ? 0 : currentIndex+1;
			} else {
				currentIndex = (currentIndex == 0) ? wayPoints.Length-1 : currentIndex-1;
			}
			
		} else {
			
			// If at the start or the end then reverse
			if((!inReverse && currentIndex+1 >= wayPoints.Length) || (inReverse && currentIndex == 0)) {
				inReverse = !inReverse;
			}
			currentIndex = (!inReverse) ? currentIndex+1 : currentIndex-1;
			
		}
		
		currentWaypoint = wayPoints[currentIndex];
		this.positioncount = this.positioncount + 1;
		CheckPosition ();
	}

	public void CheckPosition(){

		if (positioncount > Adv1.positioncount && positioncount > Adv2.positioncount) {
			Position = 1;
		}
		
		if (positioncount > Adv1.positioncount && positioncount <= Adv2.positioncount) {
			Position = 2;
		}
		
		if (positioncount <= Adv1.positioncount && positioncount > Adv2.positioncount) {
			Position = 2;
		}
		
		if (positioncount < Adv1.positioncount && positioncount < Adv2.positioncount) {
			Position = 3;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Oleo")){
			lento = true;
			Destroy (other.gameObject);
		}
		if(other.CompareTag("Fruta")){
			rapido = true;
			Destroy (other.gameObject);
		}

	}

	IEnumerator Flash(float x) {
		for (int i = 0; i < 5; i++) {
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			yield return new WaitForSeconds(x);
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
			yield return new WaitForSeconds(x);
		}
	}
	
}
