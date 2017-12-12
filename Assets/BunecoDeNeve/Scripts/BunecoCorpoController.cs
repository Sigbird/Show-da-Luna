using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunecoCorpoController : MonoBehaviour {
	private int step;
	public GameObject MenuCabeca;
	public GameObject MenuCorpo;
	public GameObject MenuBase;

	public SpriteRenderer acessorioBase;
	public SpriteRenderer acessorioCorpo;
	public SpriteRenderer acessorioCabeca;

	public SpriteRenderer spriteBase;
	public SpriteRenderer spriteCorpo;
	public SpriteRenderer spriteCabeca;

	public Sprite[] spritesCorpo;
	public Sprite[] spritesAcessorio;

	public Button NextButton;
	public Button RevertButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (step) {
		case 0:
			MenuCabeca.SetActive (false);
			MenuCorpo.SetActive (false);
			MenuBase.SetActive (true);
			RevertButton.interactable = false;
			NextButton.interactable = true;
			break;
		case 1:
			MenuCabeca.SetActive (false);
			MenuCorpo.SetActive (true);
			MenuBase.SetActive (false);
			RevertButton.interactable = true;
			NextButton.interactable = true;
			break;
		case 2:
			MenuCabeca.SetActive (true);
			MenuCorpo.SetActive (false);
			MenuBase.SetActive (false);
			RevertButton.interactable = true;
			NextButton.interactable = false;
			break;
		}
			
	}

	public void Next(){
		step++;
		GetComponent<Animator> ().SetInteger ("State", step);
	}

	public void Revert(){
		step--;
		GetComponent<Animator> ().SetInteger ("State", step);
	}

	public void SelectAcessorioItem(int x){
		switch (step) {
		case 0:
			acessorioBase.sprite = spritesAcessorio [x];
			break;
		case 1:
			acessorioCorpo.sprite = spritesAcessorio [x];
			break;
		case 2:
			acessorioCabeca.sprite = spritesAcessorio [x];
			break;
		}
	}

	public void SelectItem(int x){
		GetComponent<Animator> ().SetTrigger ("Switch");
		StartCoroutine (SwitchToItem (x));
	}

	IEnumerator SwitchToItem(int x){
		yield return new WaitForSeconds (0.30f);
		switch (step) {
		case 0:
			switch (x) {
			case 0:
				spriteBase.color = Color.white;
				break;
			case 1:
				spriteBase.color = Color.blue;
				break;
			case 2:
				spriteBase.color = Color.green;
				break;
			case 3:
				spriteBase.color = Color.yellow;
				break;
			case 4:
				spriteBase.color = Color.cyan;
				break;
			case 5:
				spriteBase.color = Color.magenta;
				break;
			}
			break;
		case 1:
			switch (x) {
			case 0:
				spriteCorpo.color = Color.white;
				break;
			case 1:
				spriteCorpo.color = Color.blue;
				break;
			case 2:
				spriteCorpo.color = Color.green;
				break;
			case 3:
				spriteCorpo.color = Color.yellow;
				break;
			case 4:
				spriteCorpo.color = Color.cyan;
				break;
			case 5:
				spriteCorpo.color = Color.magenta;
				break;
			}
			break;
		case 2:
			switch (x) {
			case 0:
				spriteCabeca.color = Color.white;
				break;
			case 1:
				spriteCabeca.color = Color.blue;
				break;
			case 2:
				spriteCabeca.color = Color.green;
				break;
			case 3:
				spriteCabeca.color = Color.yellow;
				break;
			case 4:
				spriteCabeca.color = Color.cyan;
				break;
			case 5:
				spriteCabeca.color = Color.magenta;
				break;
			}
			break;
		}
	}
		
}
