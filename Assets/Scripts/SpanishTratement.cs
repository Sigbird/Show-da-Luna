using UnityEngine;
using System.Collections;

/*
 *	ESTE SCRIPT ESTA SENDO USADO PARA REPARAR UM PROBLEMA TEMPORARIO
 *	DA FALTA DE VIDEOS CORRESPONDENTES NA VERSAO ESPANHOLA.
 *	
 *	PORTANTO DEVE SER APAGADO FUTURAMENTE.
 *	OU SEJA, GAMBIS.
 */
public class SpanishTratement : MonoBehaviour {
	
	public GameObject[] toDeactive;
	public GameObject[] toActive;
	
	void Start () {
		if (Application.systemLanguage == SystemLanguage.Spanish) {
			foreach(GameObject videoButton in toDeactive){
				videoButton.SetActive(false);
			}
			foreach(GameObject videoButton in toActive){
				videoButton.SetActive(true);
			}
		}
	}

}
