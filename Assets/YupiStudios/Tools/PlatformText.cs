using UnityEngine;
using UnityEngine.UI;

public class PlatformText : MonoBehaviour {
	[TextArea]
	public string IOSText;

	private Text textComponent;

	void Start () {
		textComponent = GetComponent<Text> ();

		#if UNITY_IOS 
		setText(IOSText);
		#endif
	}

	private void setText(string text) {
		if (!string.IsNullOrEmpty(text)) {
			textComponent.text = text;
		}			
	}
}
