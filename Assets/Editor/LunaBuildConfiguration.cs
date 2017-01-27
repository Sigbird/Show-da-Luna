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

		if (presets != null && presets.Count > 0) {					
			selected = EditorGUILayout.Popup("Select Preset:",selected, getLabelsArray());	
			ApplySelection(selected);
		}

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
	}

	public void ClearPresets() {
		Preset.ClearPresets();
		if (presets != null) presets.Clear();
	}

	public void CreatePresetsList() {	
		if (presets != null && presets.Count > 0)	 {			
			foreach (Preset preset in presets) {				
				EditorGUILayout.LabelField(preset.Name + ":");
				string purschaseType = preset.PurchaseType == BuildType.Free ? "Free" : "IAP";
				EditorGUILayout.LabelField(purschaseType);
				EditorGUILayout.Toggle("GPGS", preset.EnableGPGS);
				EditorGUILayout.Toggle("Facebook", preset.EnableFacebook);
				EditorGUILayout.Toggle("Push", preset.EnablePush);
				EditorGUILayout.Toggle("YP Button", preset.EnableYupiPlayButton);
				EditorGUILayout.Toggle("V.Downloads", preset.EnableVideoDownloads);				

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
		if (EditorSceneManager.GetActiveScene().name == "Splash") {
			Preset preset = presets[selected];

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
		if (presets != null && presets.Count > 0) {
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
