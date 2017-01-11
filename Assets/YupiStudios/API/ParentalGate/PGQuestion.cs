using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[System.Serializable]
public class PGQuestion {

	private const int MATH_MAX = 10;
	private const int MATH_MAX_2 = 2 * MATH_MAX;

	[XmlAttribute("Question")]
	public string Question;

	[XmlArray("WrongAnswers")]
	[XmlArrayItem("WrongAnswer")]
	public List<string>  WrongAnswers;

	[XmlArrayItem("CorrectAnswer")]
	public string CorrectAnswer;


	public static PGQuestion GetNewMathQuestion()
	{
		PGQuestion question = new PGQuestion ();
		int num1 = Random.Range (0, MATH_MAX+1);
		int num2 = Random.Range (0, MATH_MAX+1);
		int result = num1 + num2;
		question.Question = num1 + " + " + num2 + " = ?";
		question.CorrectAnswer = result.ToString();
		question.WrongAnswers = new List<string> ();
		for (int i = 0; i < 5; ++i) {

			int num = Random.Range (0, MATH_MAX_2);

			while (num == result || question.WrongAnswers.Contains(num.ToString()))
				num = Random.Range (0, MATH_MAX_2);

			question.WrongAnswers.Add(num.ToString());
		}

		return question;
	}

	private void ShuffleArray(string[] answerArray)
	{
		for (int i = 0; i < answerArray.Length; ++i )
		{
			string tmp = answerArray[i]; 
			int r = Random.Range(i, answerArray.Length);
			answerArray[i] = answerArray[r];
			answerArray[r] = tmp;
		}
	}

	public string [] GetWrongAnswers(int numAnswers)
	{

		if (numAnswers == WrongAnswers.Count) {

			string [] answers = WrongAnswers.ToArray ();
			ShuffleArray( answers );
			return answers;

		} else if (numAnswers > WrongAnswers.Count) {

			string [] answers = new string[numAnswers];
			WrongAnswers.CopyTo (answers);

			for (int i = 0; i < numAnswers - WrongAnswers.Count; ++i) {
				answers [WrongAnswers.Count + i] = WrongAnswers [Random.Range (0, WrongAnswers.Count)];
			}

			ShuffleArray(answers);
			return answers;
				
		} else {
			
			List<string> nList= new List<string> (WrongAnswers);

			for (int i = 0; i < WrongAnswers.Count - numAnswers; ++i)
				nList.RemoveAt( Random.Range (0, nList.Count) );

			return nList.ToArray();
		}

	}

	public string GetCorrectAnswer()
	{
		return CorrectAnswer;		
	}

}
