using UnityEngine;
using System.Collections;

public class ParentalGate : MonoBehaviour {

	protected string userAnswer;
	private string correctAnswer;
	public GameObject objectToActive;
	protected GameObject[] alternatives;

	public void CheckAnswer(string answer){
		if (answer.Equals (correctAnswer)) {
			objectToActive.SetActive (true);
		} else {
			objectToActive.SetActive(false);
		}
	}
}
