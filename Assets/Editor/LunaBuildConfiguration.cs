using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using YupiPlay.Luna;
using MiniJSON;
using System;
using UnityEditor.SceneManagement;

public class LunaBuildConfiguration : EditorWindow {	
	public class Preset {
		public string Name = "Google Play";
		public BuildType PurchaseType = BuildType.IAP;
		public bool EnableGPGS = true;
		public bool EnableFacebook = true;
		public bool EnablePush = true;
		public bool EnableYupiPlayButton = true;
		public bool EnableVideoDownloads = true;
	}		

	public static LunaBuildConfiguration MyWindow;
	public Preset newPreset;

	private Vector2 scrollPos;
	private int selected = 0;
	private BuildPreset[] presetsArray;
	private string[] labelsArray;
	private bool[] deleteArray;

	[@MenuItem ("Luna/Configuration Presets", false, 1)]
	public static void ShowWindow() {
		MyWindow = ScriptableObject.CreateInstance<LunaBuildConfiguration>();
		MyWindow.titleContent = new GUIContent("Configuration Presets");
		MyWindow.ShowUtility();
	}

	void OnGUI() {		
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		if (presetsArray != null) {									
			selected = EditorGUILayout.Popup("Select Preset:",selected, labelsArray);	
			ApplySelection(selected);
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Presets", EditorStyles.boldLabel);

		CreatePresetsList();

		EditorGUILayout.Space();
		bool clearPresets = GUILayout.Button("Clear Presets");
		if (clearPresets) {
			ClearPresets();
		}
		EditorGUILayout.EndScrollView();
	}

	void OnEnable() {
		LoadPresets();
	}

	public void ClearPresets() {
		EditorPrefs.DeleteKey("BuildPresets");
		presetsArray = null;
		labelsArray = null;
		//presetsList = null;
	}

	public void CreatePresetsList() {	
		if (presetsArray != null)	 {
			foreach (BuildPreset preset in presetsArray) {				
				EditorGUILayout.LabelField(preset.Name + ":");
				string purschaseType = preset.PurchaseType == BuildType.Free ? "Free" : "IAP";
				EditorGUILayout.LabelField(purschaseType);
				EditorGUILayout.Toggle("GPGS",preset.EnableGPGS);
				EditorGUILayout.Toggle("Facebook",preset.EnableFacebook);
				EditorGUILayout.Toggle("Push",preset.EnablePush);
				EditorGUILayout.Toggle("YP Button",preset.EnableYupiPlayButton);
				EditorGUILayout.Toggle("V.Downloads",preset.EnableVideoDownloads);						
				EditorGUILayout.Separator();
				EditorGUILayout.Space();
			}	
		}
	}

	public void LoadPresets() {
		string buildPresets = EditorPrefs.GetString("BuildPresets");
		string selectedPreset = EditorPrefs.GetString("SelectedPreset");

		if (!string.IsNullOrEmpty(buildPresets)) {
			Dictionary<string,object> presets = Json.Deserialize(buildPresets) as Dictionary<string,object>;
			presetsArray = new BuildPreset[presets.Count];
			labelsArray = new string[presets.Count];

			int i = 0;
			//Dictionary<string,object>.Enumerator enumerator = presets.GetEnumerator();

			foreach (object presetObject in presets) {				
				KeyValuePair<string,object> presetKVP = (KeyValuePair<string,object>) presetObject;
				Dictionary<string,object> preset = presetKVP.Value as Dictionary<string,object>;
				BuildPreset x = ScriptableObject.CreateInstance<BuildPreset>();

				x.Name = (string) preset["Name"];
				long purchaseType = (long) preset["PurchaseType"];
				x.PurchaseType = purchaseType == 2 ? BuildType.Free : BuildType.IAP;
				x.EnableGPGS = (bool) preset["EnableGPGS"];
				x.EnableFacebook = (bool) preset["EnableFacebook"];
				x.EnablePush = (bool) preset["EnablePush"];
				x.EnableYupiPlayButton = (bool) preset["EnableYupiPlayButton"];
				x.EnableVideoDownloads = (bool) preset["EnableVideoDownloads"];

				presetsArray[i] = x;
				labelsArray[i] = x.Name;

				if (x.Name == selectedPreset) {
					selected = i;
				} else {
					selected = 0;
				}
					
				i++;
			}
		}
	}

	private void ApplySelection(int selected) {
		if (EditorSceneManager.GetActiveScene().name == "Splash") {
			BuildPreset preset = presetsArray[selected];

			BuildConfiguration config = GameObject.FindObjectOfType<BuildConfiguration>();
			GoogleAnalyticsV3 analytics = GameObject.FindObjectOfType<GoogleAnalyticsV3>();

			config.PurchaseType = preset.PurchaseType;
			config.EnableGPGS = preset.EnableGPGS;
			config.EnableFacebook = preset.EnableFacebook;
			config.EnablePush = preset.EnablePush;
			config.EnableYupiPlayButton = preset.EnableYupiPlayButton;

			string bundleVersion = PlayerSettings.bundleVersion;

			if (preset.Name != "Google Play") {
				bundleVersion = PlayerSettings.bundleVersion + " " + preset.Name;	
			}				
			analytics.bundleVersion = bundleVersion;

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetSceneByName("Splash"));	
		}
	}

	void OnDisable() {
		if (labelsArray != null) {
			string selectedPreset = labelsArray[selected];
			EditorPrefs.SetString("SelectedPreset", selectedPreset);	
		}
	}

	private void DeletePreset(int toDelete) {		
		ArrayUtility.RemoveAt(ref presetsArray, toDelete);
		ArrayUtility.RemoveAt(ref labelsArray, toDelete);
		Debug.Log("break");
		Array.Resize(ref presetsArray, presetsArray.Length - 1);
		Array.Resize(ref labelsArray, labelsArray.Length - 1);
	}
}
