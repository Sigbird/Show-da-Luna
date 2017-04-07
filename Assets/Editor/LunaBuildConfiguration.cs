#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using YupiPlay.Luna;
using MiniJSON;
using System;
using UnityEditor.SceneManagement;

public class LunaBuildConfiguration : EditorWindow {			
	public static LunaBuildConfiguration MyWindow;

	public string BaseVersionName = "2.2.5";

	#if UNITY_ANDROID
	private const string basePackageId = "com.YupiPlay.Luna";
	#endif
	#if UNITY_IOS
	private const string basePackageId = "com.yupiplay.luna";
	#endif

	private Vector2 scrollPos;
	private int selected = 0;
	private List<Preset> presets;

	private GUILayoutOption[] options;

	[@MenuItem ("Luna/Configuration Presets", false, 1)]
	public static void ShowWindow() {
		MyWindow = EditorWindow.GetWindow<LunaBuildConfiguration>(true, "Configuration Presets");
	}

	void OnGUI() {		
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		BaseVersionName = EditorGUILayout.TextField("Bundle Version:", BaseVersionName);

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		if (presets != null && presets.Count > 0) {					
			selected = EditorGUILayout.Popup("Select Preset:", selected, getLabelsArray());
		}

		bool applyPreset = GUILayout.Button("Apply Preset");
		if (applyPreset) {
			ApplySelection(selected);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();
		bool addPreset = GUILayout.Button("Add Preset");
		if (addPreset) {
			BuildPreset presetWindow = EditorWindow.GetWindow<BuildPreset>(true, "New Preset");
			BuildPreset.MyWindow = presetWindow;
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Presets", EditorStyles.boldLabel);
		EditorGUILayout.Space();

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
		options = new GUILayoutOption[1];
		options[0] = GUILayout.MaxWidth(100f);
        BaseVersionName = EditorPrefs.GetString("CustomVersionName");

		string previouslySelected = EditorPrefs.GetString("SelectedPreset");	
		if (!String.IsNullOrEmpty(previouslySelected) && presets != null) {
			selected = presets.FindIndex(x => x.Name == previouslySelected);	
		}			
	}

	public void ClearPresets() {
		Preset.ClearPresets();
		EditorPrefs.DeleteKey("SelectedPreset");
		EditorPrefs.DeleteKey("CustomVersionName");
		if (presets != null) presets.Clear();
	}

	public void CreatePresetsList() {	
		if (presets != null && presets.Count > 0)	 {			
			foreach (Preset preset in presets) {				
				EditorGUILayout.LabelField(preset.Name + ":", EditorStyles.boldLabel);
				string purschaseType = preset.PurchaseType == BuildType.Free ? "Free" : "IAP";
				EditorGUILayout.LabelField("Package Id:", preset.PackageId);
				EditorGUILayout.LabelField(purschaseType);
				EditorGUILayout.Toggle("GPGS", preset.EnableGPGS);
				EditorGUILayout.Toggle("Facebook", preset.EnableFacebook);
				EditorGUILayout.Toggle("Push", preset.EnablePush);
				EditorGUILayout.Toggle("YP Button", preset.EnableYupiPlayButton);
				EditorGUILayout.Toggle("V.Downloads", preset.EnableVideoDownloads);
                EditorGUILayout.Toggle("Redeem", preset.EnableRedeemCode);
				EditorGUILayout.Toggle("Ads", preset.EnableAds);

                EditorGUILayout.BeginHorizontal();
				bool edit = GUILayout.Button("Edit", options);
				if (edit) {
					EditPreset(preset);
					return;
				}
				bool delete = GUILayout.Button("Delete", options);
				if (delete) {
					DeletePreset(preset);
					return;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Separator();
				EditorGUILayout.Space();
			}	
		}
	}

	public void LoadPresets() {
		presets = Preset.GetPresets();
	}

	private string[] getLabelsArray() {
		if (presets != null && presets.Count > 0) {
			string[] labels = new string[presets.Count];

			for (int i = 0; i < presets.Count; i++) {
				labels[i] = presets[i].Name;
			}
			return labels;
		}

		return null;
	}

	private void ApplySelection(int selected) {		
        if (selected >= presets.Count && presets.Count > 0) {
            selected = 0;
        }
		Preset preset = presets[selected];

        EditorPrefs.SetString("CustomVersionName", BaseVersionName);
        PlayerSettings.applicationIdentifier = basePackageId + preset.PackageId;

        string bundleVersion = BaseVersionName;
        if (preset.Name != "Google Play" && preset.Name != "Apps Club")
        {
            bundleVersion = BaseVersionName + " " + preset.Name;
        }

        PlayerSettings.bundleVersion = bundleVersion;

        if (EditorSceneManager.GetActiveScene().name == "Splash") {
			BuildConfiguration config = GameObject.FindObjectOfType<BuildConfiguration>();
			GoogleAnalyticsV3 analytics = GameObject.FindObjectOfType<GoogleAnalyticsV3>();

			config.PurchaseType = preset.PurchaseType;
			config.EnableGPGS = preset.EnableGPGS;
			config.EnableFacebook = preset.EnableFacebook;
			config.EnablePush = preset.EnablePush;
			config.EnableYupiPlayButton = preset.EnableYupiPlayButton;
			config.EnableVideoDownloads = preset.EnableVideoDownloads;
            config.EnableRedeemCode = preset.EnableRedeemCode;
			config.EnableAds = preset.EnableAds;
						
			analytics.bundleVersion = bundleVersion;
            EditorUtility.SetDirty(analytics);

			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetSceneByName("Splash"));
			EditorSceneManager.SaveOpenScenes();
		}
	}

	void OnDisable() {
		if (presets != null && presets.Count > 0 && selected < presets.Count) {
			string selectedPreset = presets[selected].Name;
			EditorPrefs.SetString("SelectedPreset", selectedPreset);	
		}
	}	

	private void DeletePreset(Preset preset) {
		presets.Remove(preset);
		Preset.RemovePreset(preset.Name);
	}

	private void EditPreset(Preset preset) {
		BuildPreset presetWindow = EditorWindow.GetWindow<BuildPreset>(true, "Edit Preset");
		BuildPreset.MyWindow = presetWindow;
		presetWindow.Edit(preset);
	}
}
#endif