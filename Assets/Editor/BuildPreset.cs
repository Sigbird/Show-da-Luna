﻿using System.Collections;
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
	private bool edit;

	public void OnGUI() {
		if (edit) {
			EditorGUILayout.LabelField(Name, EditorStyles.boldLabel);
		} else {
			Name = EditorGUILayout.TextField("Preset Name", Name);	
		}
		EditorGUILayout.Space();

		PurchaseType = (BuildType)EditorGUILayout.EnumPopup("Purchase Type", PurchaseType);

		purchaseType = PurchaseType == BuildType.Free ? 2 : 1;

		EnableGPGS = EditorGUILayout.Toggle("Enable GPGS", EnableGPGS);
		EnableFacebook = EditorGUILayout.Toggle("Enable Facebook", EnableFacebook);
		EnablePush = EditorGUILayout.Toggle("Enable Push", EnablePush);
		EnableYupiPlayButton = EditorGUILayout.Toggle("Enable Yupi Play Button", EnableYupiPlayButton);
		EnableVideoDownloads = EditorGUILayout.Toggle("Enable Video Downloads", EnableVideoDownloads);

		bool savePreset = GUILayout.Button("Save Preset");

		if (savePreset) {	
			Preset preset = new Preset(Name, PurchaseType, EnableGPGS, EnableFacebook, EnablePush, EnableYupiPlayButton,
				EnableVideoDownloads);

			Preset.Save(preset);
			MyWindow.Close();
			LunaBuildConfiguration presetsWindow = EditorWindow.GetWindow<LunaBuildConfiguration>(true, "Configuration Presets");
			presetsWindow.LoadPresets();
		}
	}			

	public void Edit(Preset preset) {
		edit = true;
		Name = preset.Name;
		PurchaseType = preset.PurchaseType;
		EnableGPGS = preset.EnableGPGS;
		EnableFacebook = preset.EnableFacebook;
		EnablePush = preset.EnablePush;
		EnableYupiPlayButton = preset.EnableYupiPlayButton;
		EnableVideoDownloads = preset.EnableVideoDownloads;
	}

	[@MenuItem ("Luna/New Preset", false, 2)]
	public static void ShowWindow() {
		MyWindow = EditorWindow.GetWindow<BuildPreset>(true, "New Preset");
	}
}