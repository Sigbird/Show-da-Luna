using UnityEngine;
using System.Collections;
using System.IO;

public class OptionsScript : MonoBehaviour {
	public GameObject feedbackWindow;
	private string pathVideos;
	private bool pathExists;

	void Start(){
		pathVideos = Application.persistentDataPath + "/videos";
		pathExists = Directory.Exists (pathVideos);
	}

	public void DeleteContent(){
		try {
			if (pathExists)
				Directory.Delete (pathVideos, true);
			feedbackWindow.SetActive(true);
		} catch (DirectoryNotFoundException e) {
			Debug.Log (e.StackTrace);
		}
	}

}