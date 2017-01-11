using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;


[System.Serializable]
[XmlRoot("ParentalBaseDatabase")]
public class PGDatabase {

	private const string XMLFILE = "PGDatabase.xml";

	[XmlArray("Questions")]
	[XmlArrayItem("Question")]
	public PGQuestion []Questions;

	List<PGQuestion> SerQuestions;

	public void SaveXML() 
	{
		string path = Application.dataPath + "/Resources/" + XMLFILE;
		var serializer = new XmlSerializer(typeof(PGDatabase));
		var stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}

	public void LoadXML() 
	{

		string path = Application.dataPath + "/Resources/" + XMLFILE;
		var serializer = new XmlSerializer(typeof(PGDatabase));
		var stream = new FileStream(path, FileMode.Open);
		PGDatabase db = serializer.Deserialize(stream) as PGDatabase;
		this.Questions = db.Questions;
		stream.Close();
	}

	public void Serialize()
	{
	}

	public void DeSerialize()
	{
	}

}
