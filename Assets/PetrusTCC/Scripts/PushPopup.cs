using UnityEngine;
using UnityEngine.UI;
using YupiStudiosOld.YupiPlay.Plugin;

public class PushPopup : MonoBehaviour {
	public GameObject Popup;
	public Text pushText;
	private YupiPlayPlugin plugin;

	// Use this for initialization
	void Start () {
		plugin = YupiPlayPlugin.getInstance();
		string message = plugin.getPushMessage();
		Debug.Log (message);

		if (message != null) {
			pushText.text = message;
			Popup.SetActive(true);
		}
	}

	public void ClosePopup() {
		Popup.SetActive(false);
	}
	

}
