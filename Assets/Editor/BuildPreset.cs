using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using YupiPlay.Luna;
using MiniJSON;

public class BuildPreset : EditorWindow {
	public string Name = "Google Play";
	public BuildType PurchaseType = BuildType.IAP;
	public bool EnableGPGS = true;
	public bool EnableFacebook = true;
	public bool EnablePush = true;
	public bool EnableYupiPlayButton = true;
	public bool EnableVideoDownloads = true;

	private int purchaseType;

	public static BuildPreset MyWindow;

	public void OnGUI() {
		Name = EditorGUILayout.TextField("Preset Name", Name);
		PurchaseType = (BuildType)EditorGUILayout.EnumPopup("Purchase Type", PurchaseType);

		purchaseType = PurchaseType == BuildType.Free ? 2 : 1;

		EnableGPGS = EditorGUILayout.Toggle("Enable GPGS", EnableGPGS);
		EnableFacebook = EditorGUILayout.Toggle("Enable Facebook", EnableFacebook);
		EnablePush = EditorGUILayout.Toggle("Enable Push", EnablePush);
		EnableYupiPlayButton = EditorGUILayout.Toggle("Enable Yupi Play Button", EnableYupiPlayButton);
		EnableVideoDownloads = EditorGUILayout.Toggle("Enable Video Downloads", EnableVideoDownloads);

		bool savePreset = GUILayout.Button("Save Preset");

		if (savePreset) {			
			Save(Name, ToDictionary());
		}
	}

	public Dictionary<string,object> ToDictionary() {
		Dictionary<string,object> jsonObject = new Dictionary<string,object>();
		jsonObject["Name"] = (object) Name;
		jsonObject["PurchaseType"] = (object) purchaseType;
		jsonObject["EnableGPGS"] = (object) EnableGPGS;
		jsonObject["EnableFacebook"] = (object) EnableFacebook;
		jsonObject["EnablePush"] = (object) EnablePush;
		jsonObject["EnableYupiPlayButton"] = (object) EnableYupiPlayButton;
		jsonObject["EnableVideoDownloads"] = (object) EnableVideoDownloads;

		return jsonObject;
	}

	public void Save(string name, Dictionary<string,object> dict) {
		string buildPresetsString = EditorPrefs.GetString("BuildPresets");
		Dictionary<string,object> buildPresets;
		string presetsString;

		if (string.IsNullOrEmpty(buildPresetsString)) {
			buildPresets = new Dictionary<string,object>();
			buildPresets[name] = dict;
			presetsString = Json.Serialize(buildPresets);
			EditorPrefs.SetString("BuildPresets", presetsString);
			MyWindow.Close();
			return;
		}

		buildPresets = Json.Deserialize(buildPresetsString) as Dictionary<string,object>;
		buildPresets[name] = dict;
		presetsString = Json.Serialize(buildPresets);
		EditorPrefs.SetString("BuildPresets", presetsString);
		MyWindow.Close();
	}					

	[@MenuItem ("Luna/New Preset", false, 2)]
	public static void ShowWindow() {
		MyWindow = ScriptableObject.CreateInstance<BuildPreset>();
		MyWindow.titleContent = new GUIContent("New Preset");
		MyWindow.ShowUtility();
	}
}
