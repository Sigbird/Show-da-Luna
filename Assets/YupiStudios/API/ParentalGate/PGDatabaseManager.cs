using UnityEngine;
using System.Collections;

public class PGDatabaseManager : MonoBehaviour {

	public bool allowMathQuestions;

	public PGDatabase database;



	public PGQuestion GetRandomQuestion()
	{
		int dbLength = database.Questions.Length;

		if (dbLength == 0)
			return PGQuestion.GetNewMathQuestion();

		if (allowMathQuestions)
		{
			bool isMath = Random.Range (0, 2) == 0;
			if (isMath)
				return PGQuestion.GetNewMathQuestion();
		}

		if (dbLength > 0)
			return database.Questions [Random.Range (0, dbLength)];
		else 
			return null;
	}

	public void Load ()
	{
		database.LoadXML ();
	}

	public void Save ()
	{
		database.SaveXML ();
	}
}
