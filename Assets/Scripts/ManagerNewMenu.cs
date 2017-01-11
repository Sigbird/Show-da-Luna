using UnityEngine;
using System.Collections;

public class ManagerNewMenu : MonoBehaviour {
	public float DelayToLoadScene = 0.9f;

    public void EnterScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            Application.LoadLevel(sceneToLoad);
    }

	public void EnterSceneWithDelay(string sceneToLoad) {
		StartCoroutine(EnterSceneWithDelayCorutine(sceneToLoad));
	}

	public IEnumerator EnterSceneWithDelayCorutine(string sceneToLoad) {
		yield return new WaitForSeconds(DelayToLoadScene);
		EnterScene(sceneToLoad);
	}
	
}
