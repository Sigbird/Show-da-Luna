using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour {
	public float FlySpeed = 3f;
	public float MaxFlySpeedY = 7f;
	public float MaxGravitySpeed = 4f;
	public Text SpeedYUI;
	public Text SpeedXUI;

	public AudioSource Flap;
	public AudioSource Hit;

	private Camera mainCamera;
	private Rigidbody2D body;
	private SpriteRenderer sprite;

	private float bottom;
	private float left;
	private float right;
	private float top;

	private Animator animator;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		body = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		//limites do ViewPort;
		bottom = mainCamera.ViewportToWorldPoint(new Vector3(0,0,mainCamera.transform.position.z)).y;
		left = mainCamera.ViewportToWorldPoint(new Vector3(0,0,mainCamera.transform.position.z)).x;
		right = mainCamera.ViewportToWorldPoint(new Vector3(1,1,mainCamera.transform.position.z)).x;
		top = mainCamera.ViewportToWorldPoint(new Vector3(1,1,mainCamera.transform.position.z)).y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//quica nas bordas
		//só para teste e não ter que fazer cenário
		if (transform.position.y <= bottom) {			
			body.velocity = new Vector2(body.velocity.x, FlySpeed);
			HitAction();
			return;
		}
		if (transform.position.y >= top) {			
			body.velocity = new Vector2(body.velocity.x, -FlySpeed);
			HitAction();
			return;
		}
		if (transform.position.x <= left) {			
			body.velocity = new Vector2(FlySpeed, body.velocity.y);
			HitAction();
			return;
		}
		if (transform.position.x >= right) {			
			body.velocity = new Vector2(-FlySpeed, body.velocity.y);
			HitAction();
			return;
		}							

		//A velocidade X é limitadas pelo Linear Drag do Rigidbody2D
		//A velocidade Y limitada aqui
		if (body.velocity.y <= -MaxGravitySpeed) {
			body.velocity = new Vector2(body.velocity.x, -MaxGravitySpeed);
		}			
	}					

	void Update() {
		if (SpeedXUI != null && SpeedYUI != null) {
			SpeedYUI.text = body.velocity.y.ToString();
			SpeedXUI.text = body.velocity.x.ToString();	
		}
	}

	//velocidade máxima para esquerda e direita é constante
	public void FlyLeft() {
		Debug.Log("left");

		body.velocity = new Vector2(-FlySpeed, getFlySpeedY());
		sprite.flipX = false;
		FlapAction();
	}

	public void FlyRight() {
		Debug.Log("right");

		body.velocity = new Vector2(FlySpeed, getFlySpeedY());
		sprite.flipX = true;
		FlapAction();
	}

	//soma impulso para cima até o limite de velocidade para cima
	private float getFlySpeedY() {
		return Mathf.Clamp(body.velocity.y <= 0 ? FlySpeed : body.velocity.y + FlySpeed, 0, MaxFlySpeedY);
	}

	private void FlapAction() {
		animator.SetTrigger("Flap");
		Flap.Play();
	}

	private void HitAction() {
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit")) {
			animator.SetTrigger("Hit");	
		}

		Hit.Play();
	}		
}
