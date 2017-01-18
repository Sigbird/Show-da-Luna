using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour {
	public float FlySpeed = 5f;
	public float MaxFlySpeedY = 5;
	public float MaxGravitySpeed = 5f;
	public float XResFactor = 0.2f;
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
	void Update () {
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

		//As velocidades X e Y são limitadas pelo Linear Drag do Rigidbody2D

		SpeedYUI.text = body.velocity.y.ToString();
		SpeedXUI.text = body.velocity.x.ToString();
	}		

	private void CameraFollow() {
		Vector3 pos = transform.position;
		mainCamera.transform.position = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
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

	void LateUpdate() {
		//CameraFollow();
	}
}
