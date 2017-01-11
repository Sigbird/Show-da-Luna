using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class PGUIController : MonoBehaviour {

	public PGDatabaseManager database;

	public Text Question;
	public Text [] Answers = new Text[6];

	public UnityEvent OnEnableEvent;
	public UnityEvent CorrectAnswerEvent;
	public UnityEvent WrongAnswerEvent;

	private int CorrectAnswerIndex;

	public void SelectOption(int index)
	{
		if (index == CorrectAnswerIndex) {
			if (CorrectAnswerEvent != null)
				CorrectAnswerEvent.Invoke ();
		} else {
			if (WrongAnswerEvent != null)
				WrongAnswerEvent.Invoke ();
		}
	}

	public void Refresh()
	{
		CorrectAnswerIndex = Random.Range (1,Answers.Length);
		PGQuestion dbQuestion = database.GetRandomQuestion ();
		string [] wrongAnswers = dbQuestion.GetWrongAnswers (Answers.Length-1);
		
		for (int j = 0, i = 0; i < Answers.Length; ++i) {
			if (i != CorrectAnswerIndex)
			{
				Answers[i].text = wrongAnswers[j++];
			} else 
			{
				Answers[i].text = dbQuestion.CorrectAnswer;
			}
		}
		
		Question.text = dbQuestion.Question;
	}

	// Use this for initialization
	void OnEnable () {

		if (OnEnableEvent != null)
			OnEnableEvent.Invoke ();

		Refresh();
	
	}

}
