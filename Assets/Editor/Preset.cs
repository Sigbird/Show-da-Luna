#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.Luna;
using UnityEditor;
using MiniJSON;

public class Preset {
	public string Name = "Google Play";
	public string PackageId = "";
	public BuildType PurchaseType = BuildType.IAP;
	public bool EnableGPGS = true;
	public bool EnableFacebook = true;
	public bool EnablePush = true;
	public bool EnableYupiPlayButton = true;
	public bool EnableVideoDownloads = true;
    public bool EnableRedeemCode = true;
	public bool EnableAds = true;

    public Preset(string name, string packageId, BuildType purchaseType, bool enableGPGS, bool enableFacebook, bool enablePush, 
		bool enableYupiPlayButton, bool enableVideoDownloads, bool enableRedeemCode, bool enableAds) {
		Name = name;
		PackageId = packageId;
		PurchaseType = purchaseType;
		EnableGPGS = enableGPGS;
		EnableFacebook = enableFacebook;
		EnablePush = enablePush;
		EnableYupiPlayButton = enableYupiPlayButton;
		EnableVideoDownloads = enableVideoDownloads;
        EnableRedeemCode = enableRedeemCode;        
		EnableAds = enableAds;
	}

	public Preset() {}

	public static void Save(Preset preset) {
		Dictionary<string, object> dict = ToDictionary(preset);

		string buildPresetsString = EditorPrefs.GetString("BuildPresets");
		Dictionary<string,object> buildPresets;
		string presetsString;

		if (string.IsNullOrEmpty(buildPresetsString)) {
			buildPresets = new Dictionary<string,object>();
			buildPresets[preset.Name] = dict;
			presetsString = Json.Serialize(buildPresets);
			EditorPrefs.SetString("BuildPresets", presetsString);
			return;
		}

		buildPresets = Json.Deserialize(buildPresetsString) as Dictionary<string,object>;
		buildPresets[preset.Name] = dict;
		presetsString = Json.Serialize(buildPresets);
		EditorPrefs.SetString("BuildPresets", presetsString);
	}

	private static Dictionary<string,object> ToDictionary(Preset preset) {
		Dictionary<string,object> jsonObject = new Dictionary<string,object>();
		jsonObject["Name"] = (object) preset.Name;
		jsonObject["PackageId"] = (object) preset.PackageId;

		int purchaseType = preset.PurchaseType == BuildType.Free ? 2 : 1;
		jsonObject["PurchaseType"] = (object) purchaseType;

		jsonObject["EnableGPGS"] = (object) preset.EnableGPGS;
		jsonObject["EnableFacebook"] = (object) preset.EnableFacebook;
		jsonObject["EnablePush"] = (object) preset.EnablePush;
		jsonObject["EnableYupiPlayButton"] = (object) preset.EnableYupiPlayButton;
		jsonObject["EnableVideoDownloads"] = (object) preset.EnableVideoDownloads;
        jsonObject["EnableRedeemCode"] = (object) preset.EnableRedeemCode;
		jsonObject["EnableAds"] = (object) preset.EnableAds;

		return jsonObject;
	}

	public static void ClearPresets() {
		EditorPrefs.DeleteKey("BuildPresets");
	}

	public static string GetSelectedPreset() {
		return EditorPrefs.GetString("SelectedPreset");
	}

	public static List<Preset> GetPresets() {
		string buildPresets = EditorPrefs.GetString("BuildPresets");

		if (!string.IsNullOrEmpty(buildPresets)) {
			Dictionary<string,object> presets = Json.Deserialize(buildPresets) as Dictionary<string,object>;
			List<Preset> presetList = new List<Preset>();

			foreach (object presetObject in presets) {				
				KeyValuePair<string,object> presetKVP = (KeyValuePair<string,object>) presetObject;
				Dictionary<string,object> preset = presetKVP.Value as Dictionary<string,object>;
				Preset x = new Preset();

				x.Name = (string) preset["Name"];
				x.PackageId = (string) preset["PackageId"];
				long purchaseType = (long) preset["PurchaseType"];
				x.PurchaseType = purchaseType == 2 ? BuildType.Free : BuildType.IAP;
				x.EnableGPGS = (bool) preset["EnableGPGS"];
				x.EnableFacebook = (bool) preset["EnableFacebook"];
				x.EnablePush = (bool) preset["EnablePush"];
				x.EnableYupiPlayButton = (bool) preset["EnableYupiPlayButton"];
				x.EnableVideoDownloads = (bool) preset["EnableVideoDownloads"];

                if (preset.ContainsKey("EnableRedeemCode")) {
                    x.EnableRedeemCode = (bool) preset["EnableRedeemCode"];
                } else { x.EnableRedeemCode = false; }

				if (preset.ContainsKey("EnableAds")) {
					x.EnableAds = (bool) preset["EnableAds"];
				} else { x.EnableAds = false; }

				presetList.Add(x);
			}

			return presetList;
		}

		return null;
	}

	public static void RemovePreset(string name) {
		string buildPresets = EditorPrefs.GetString("BuildPresets");
		if (!string.IsNullOrEmpty(buildPresets)) {
			Dictionary<string,object> presets = Json.Deserialize(buildPresets) as Dictionary<string,object>;
			presets.Remove(name);

			string presetsString = Json.Serialize(presets);
			EditorPrefs.SetString("BuildPresets", presetsString);
		}
	}		
}
#endif