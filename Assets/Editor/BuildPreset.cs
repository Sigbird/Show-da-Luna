#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using YupiPlay.Luna;

public class BuildPreset : EditorWindow {	
	public static BuildPreset MyWindow;
	private bool edit;

    private Preset preset;

	[@MenuItem ("Luna/New Preset", false, 2)]
	public static void ShowWindow() {
		MyWindow = EditorWindow.GetWindow<BuildPreset>(true, "New Preset");
	}

	public void OnGUI() {
        if (preset == null) {
            preset = new Preset();
        }       

		if (edit) {
			EditorGUILayout.LabelField(preset.Name, EditorStyles.boldLabel);
		} else {
            preset.Name = EditorGUILayout.TextField("Preset Name", preset.Name);	
		}			
		EditorGUILayout.Space();

		preset.PackageId = EditorGUILayout.TextField("Package Id:", preset.PackageId);

		preset.PurchaseType = (BuildType)EditorGUILayout.EnumPopup("Purchase Type", preset.PurchaseType);		

		preset.EnableGPGS = EditorGUILayout.Toggle("Enable GPGS", preset.EnableGPGS);
		preset.EnableFacebook = EditorGUILayout.Toggle("Enable Facebook", preset.EnableFacebook);
		preset.EnablePush = EditorGUILayout.Toggle("Enable Push", preset.EnablePush);
		preset.EnableYupiPlayButton = EditorGUILayout.Toggle("Enable Yupi Play Button", preset.EnableYupiPlayButton);
		preset.EnableVideoDownloads = EditorGUILayout.Toggle("Enable Video Downloads", preset.EnableVideoDownloads);
        preset.EnableRedeemCode = EditorGUILayout.Toggle("Enable Redeem Code", preset.EnableRedeemCode);

        bool savePreset = GUILayout.Button("Save Preset");

        if (savePreset) {           
            Preset.Save(preset);
            MyWindow.Close();
            LunaBuildConfiguration presetsWindow = EditorWindow.GetWindow<LunaBuildConfiguration>(true, "Configuration Presets");
            presetsWindow.LoadPresets();
        }
    }			

	public void Edit(Preset preset) {
		edit = true;
        this.preset = preset;		
	}		
}
#endif