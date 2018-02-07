﻿using UnityEngine;
using System.Collections;
using System.IO;

public class NativeShareScript : MonoBehaviour {
	public GameObject CanvasShareObj;
	public GameObject CanvasHideObj;

	private bool isProcessing = false;
	private bool isFocus = false;

	public void ShareBtnPress()
	{
		if (!isProcessing)
		{
			CanvasHideObj.SetActive (false);
			CanvasShareObj.SetActive(true);
			StartCoroutine(ShareScreenshot());
		}
	}

	IEnumerator ShareScreenshot()
	{
		#if UNITY_ANDROID 
		isProcessing = true;

		yield return new WaitForEndOfFrame();

		ScreenCapture.CaptureScreenshot("screenshot.png", 2);
		string destination = Path.Combine(Application.persistentDataPath, "screenshot.png");

		yield return new WaitForSecondsRealtime(0.3f);

		if (!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
				uriObject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
				"Feliz Natal e Boas Festas!");
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
				intentObject, "Share your new score");
			currentActivity.Call("startActivity", chooser);

			yield return new WaitForSecondsRealtime(1);
		}

		yield return new WaitUntil(() => isFocus);
		CanvasShareObj.SetActive(false);
		CanvasHideObj.SetActive (true);
		isProcessing = false;
		#endif
	}

	private void OnApplicationFocus(bool focus)
	{
		isFocus = focus;
	}
}