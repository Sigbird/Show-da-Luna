using UnityEngine;
using System.Collections;
using YupiPlay.Luna;
using UnityEngine.SceneManagement;

public class ManagerNewMenu : MonoBehaviour {
	public float DelayToLoadScene = 0.9f;

	public GameObject OptionsButton;

    public void EnterScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
			SceneManager.LoadScene(sceneToLoad);
            //Application.LoadLevel(sceneToLoad);
    }

	public void EnterSceneWithDelay(string sceneToLoad) {
		StartCoroutine(EnterSceneWithDelayCorutine(sceneToLoad));
	}

	public IEnumerator EnterSceneWithDelayCorutine(string sceneToLoad) {
		yield return new WaitForSeconds(DelayToLoadScene);
		EnterScene(sceneToLoad);
	}

	public void EnterVideoGallery() {
		if (!BuildConfiguration.VideoDownloadsEnabled) {
			EnterScene("VideoGalleryOffline");
		} else {
			EnterScene("VideoGallery");
		}
	}	

	void Awake() {
		if (BuildConfiguration.GPGSEnabled || BuildConfiguration.VideoDownloadsEnabled 
			|| BuildConfiguration.CurrentPurchaseType == BuildType.IAP) {
			return;
		} else {
			if (OptionsButton != null) {
				OptionsButton.transform.parent = null;
				Destroy(OptionsButton);	
			}

		}
	}
}
