using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OfflineVideosEditor : EditorWindow {
	public static OfflineVideosEditor MyWindow;

	[@MenuItem ("Luna/Offline Videos", false, 3)]
	public static void ShowWindow() {
		MyWindow = EditorWindow.GetWindow<OfflineVideosEditor>(true, "Offline Videos Editor");
	}

	void OnGUI() {
		
	}


}
